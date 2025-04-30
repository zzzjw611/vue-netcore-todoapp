<template>
  <!--  flex layout,hover effects -->
  <li
    class="flex items-center justify-between p-4 bg-white rounded-lg shadow-sm
           hover:shadow-md hover:bg-gray-100 transition"
  >
    <div class="flex items-center gap-3">
      <!-- Toggle button: flip completed state -->
      <button
        @click="$emit('toggle', todo)"
        class="w-6 h-6 flex items-center justify-center rounded-full transition-colors
               focus:outline-none focus:ring-2 focus:ring-indigo-300"
        :class="todo.completed
          ? 'bg-green-500 text-white hover:bg-green-600'
          : 'border-2 border-gray-400 text-gray-400 hover:bg-gray-200'"
      >
        <!-- Show checkmark if completed -->
        {{ todo.completed ? '✔︎' : '' }}
      </button>

      <!-- Task title: applies line-through styling when completed -->
      <span
        :class="todo.completed
          ? 'line-through text-gray-400'
          : 'text-gray-800'"
        class="font-medium"
      >
        {{ todo.title }}
      </span>
    </div>

    <!-- Delete button: emits "remove" event with the todo's id -->
    <button
      @click="$emit('remove', todo.id)"
      class="text-gray-400 hover:text-red-600 hover:bg-red-100 rounded-full p-1
             transition focus:outline-none focus:ring-2 focus:ring-red-300"
    >
      🗑
    </button>
  </li>
</template>

<script setup>
const props = defineProps({
  todo: { type: Object, required: true }
})

const emit = defineEmits(['toggle', 'remove'])
</script>
