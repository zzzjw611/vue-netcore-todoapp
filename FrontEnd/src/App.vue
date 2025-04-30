<!-- src/App.vue -->
<template>
  <!-- 1. Full-screen background container -->
  <div class="fixed inset-0 bg-gradient-to-br from-blue-50 to-green-50 overflow-auto">
    <!-- 2.  light blobs -->
    <div
      class="absolute -top-16 -left-16 w-40 h-40 bg-blue-200 rounded-full mix-blend-multiply filter blur-3xl opacity-40"
    ></div>
    <div
      class="absolute -bottom-16 -right-16 w-56 h-56 bg-green-200 rounded-full mix-blend-multiply filter blur-4xl opacity-30"
    ></div>

    <!-- 3. Content wrapper -->
    <div class="relative flex flex-col items-center justify-center min-h-screen p-4 sm:p-8">
      <!-- Welcome screen -->
      <ModeSelector v-if="!mode" @select="mode = $event" />

      <!-- To-Do list page -->
      <div v-else class="w-full max-w-screen-md mx-auto space-y-6">
        <!-- Back button -->
        <button
          @click="mode = null"
          class="inline-flex items-center gap-1 text-gray-600 hover:text-gray-800 transition"
        >
          ← Prev
        </button>
        <!-- The actual To-Do application component -->
        <TodoApp :provider="provider" :mode="mode" />
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, computed } from 'vue'
import ModeSelector from './components/ModeSelector.vue'
import TodoApp from './components/TodoApp.vue'
import InMemoryProvider from './providers/InMemoryProvider.js'
import {
  setProviderMode,
  getTodos,
  addTodo,
  updateTodo,
  deleteTodo
} from './services/todoService.js'

// Holds the current mode: null / 'user' / 'guest'
const mode = ref(null)

// Chooses the appropriate provider based on mode.
// - Guest: front-end in-memory provider (max 5 items).
// - User: back-end via HTTP (persisted up to 1 month).
const provider = computed(() => {
  if (mode.value === 'guest') {
    return new InMemoryProvider()
  }
  if (mode.value === 'user') {
    setProviderMode('user')
    return { getTodos, addTodo, updateTodo, deleteTodo }
  }
  return null
})
</script>
