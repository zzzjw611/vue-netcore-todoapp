<template>
  <!-- Wrapper: centered card with max-width and padding -->
  <div class="max-w-xl w-full mx-auto p-6 bg-white rounded-2xl shadow-xl">
    <!-- Title: show different headings for user vs guest mode -->
    <h2 class="text-2xl font-semibold text-gray-800 mb-6 text-center">
      {{ mode === 'user' ? 'Your To-Do List' : 'Guest To-Do / Store 5 lists' }}
    </h2>

    <!-- Input area: new task entry, add button, and search field -->
    <div class="flex gap-3 mb-6">
      <!-- Text input for a new todo; 'Enter' triggers add -->
      <input
        v-model="newText"
        @keyup.enter="add"
        placeholder="Add a new task…"
        class="flex-1 px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-400 transition"
      />
      <!-- Add button for creating a new todo -->
      <button
        @click="add"
        class="px-5 py-2 bg-green-200 text-green-800 rounded-lg hover:bg-green-300 transition"
      >
        Add
      </button>
      <!-- Search input with debounce to filter todos -->
      <input
        v-model="searchTerm"
        @input="onSearch"
        placeholder="Search…"
        class="w-32 px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-green-400 transition"
      />
    </div>

    <!-- Filter buttons: All, Completed, Not -->
    <div class="flex gap-2 justify-center mb-6">
      <!-- Show all tasks -->
      <button
        @click="filter = 'all'"
        :class="filter==='all'
          ? 'bg-green-300 text-white'
          : 'bg-gray-100 text-gray-600 hover:bg-gray-200'"
        class="px-4 py-1 rounded-full transition"
      >All 📝</button>

      <!-- Show completed tasks -->
      <button
        @click="filter = 'completed'"
        :class="filter==='completed'
          ? 'bg-green-300 text-white'
          : 'bg-gray-100 text-gray-600 hover:bg-gray-200'"
        class="px-4 py-1 rounded-full transition"
      >Completed 👌</button>

      <!-- Show only not completed tasks -->
      <button
        @click="filter = 'active'"
        :class="filter==='active'
          ? 'bg-green-300 text-white'
          : 'bg-gray-100 text-gray-600 hover:bg-gray-200'"
        class="px-4 py-1 rounded-full transition"
      >Not Completed 🥲</button>
    </div>

    <!-- Task list: iterate over filteredTodos -->
    <ul class="space-y-3">
      <li
        v-for="t in filteredTodos"
        :key="t.id"
        class="flex items-center justify-between p-4 bg-gray-50 rounded-lg hover:bg-gray-100 transition"
      >
        <div class="flex items-center gap-3">
          <!-- Toggle button: mark as completed or active -->
          <button
            @click="toggle(t)"
            class="w-6 h-6 flex items-center justify-center rounded-full transition"
            :class="t.completed
              ? 'bg-blue-600 text-white'
              : 'border-2 border-gray-400 text-gray-400 hover:bg-gray-200'"
          >
            {{ t.completed ? '✅' : '⚪' }}
          </button>
          <!-- Task title with line-through style when completed -->
          <span
            :class="t.completed
              ? 'line-through text-gray-400'
              : 'text-gray-800'"
            class="font-medium"
          >
            {{ t.title }}
          </span>
        </div>
        <!-- Delete button: remove the task -->
        <button
          @click="remove(t.id)"
          class="text-gray-400 hover:text-red-500 transition"
        >🗑 Delete</button>
      </li>
    </ul>
  </div>
</template>

<script setup>
import { ref, computed, onMounted } from 'vue'
// Debounce function 
import debounce from 'lodash.debounce'

// Define incoming props: provider (for data access) and mode (user/guest)
const props = defineProps({
  provider: Object,
  mode: String
})

const emit = defineEmits(['back'])
// Handler to emit 'back' event
const goBack = () => emit('back')

// Reactive state variables
const todos = ref([])         // Complete list from provider
const newText = ref('')       // New task input
const searchTerm = ref('')    // Search input
const filter = ref('all')     // Current filter


const load = async (search = '') => {
  todos.value = await props.provider.getTodos(search)
}
// Initial load on component mount
onMounted(() => load())

// Add a new task
const add = async () => {
  if (!newText.value.trim()) return
  await props.provider.addTodo({ title: newText.value, completed: false })
  newText.value = ''
  await load(searchTerm.value)
}

// Toggle completed status
const toggle = async (t) => {
  await props.provider.updateTodo(t.id, { ...t, completed: !t.completed })
  await load(searchTerm.value)
}

// Remove a task by ID
const remove = async (id) => {
  await props.provider.deleteTodo(id)
  await load(searchTerm.value)
}

// Debounced search 
const onSearch = debounce(() => {
  load(searchTerm.value)
}, 300)

const filteredTodos = computed(() => {
  let list = todos.value
  // Text search filter
  if (searchTerm.value) {
    const key = searchTerm.value.toLowerCase()
    list = list.filter(t => t.title.toLowerCase().includes(key))
  }
  if (filter.value === 'completed') {
    list = list.filter(t => t.completed)
  } else if (filter.value === 'active') {
    list = list.filter(t => !t.completed)
  }
  return list
})
</script>
