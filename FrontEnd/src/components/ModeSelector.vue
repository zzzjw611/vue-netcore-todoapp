<!-- src/components/ModeSelector.vue -->
<template>
  <!--
    Full-screen centered background for 1: Mode Selection.
    Applies a soft gradient and extra padding for all screen sizes.
  -->
  <div
    class="fixed inset-0 bg-gradient-to-br from-blue-50 to-green-50 flex items-center justify-center p-4 sm:p-8"
  >
    <!-- Decorative blurred circles in the background (only on md+ screens) -->
    <div
      class="hidden md:block absolute -top-16 -left-16 w-32 h-32 bg-blue-200 rounded-full mix-blend-multiply filter blur-3xl opacity-40"
    ></div>
    <div
      class="hidden md:block absolute -bottom-16 -right-16 w-48 h-48 bg-green-200 rounded-full mix-blend-multiply filter blur-4xl opacity-30"
    ></div>

    <!-- Glassmorphism card container -->
    <div class="relative w-full max-w-screen-md mx-auto">
      <div class="bg-white/60 backdrop-blur-lg rounded-3xl shadow-2xl p-6 sm:p-8 md:p-10">
        <!-- Two-column grid: single column on small screens, two columns on medium+ -->
        <div class="grid grid-cols-1 md:grid-cols-2 gap-8 items-center">
          
          <!-- Left side: Headline + Buttons -->
          <div class="space-y-6">
            <h1 class="text-3xl sm:text-4xl md:text-5xl font-extrabold text-gray-900">
              Welcome!<br />
              Let’s make Everything Easier!
            </h1>

            <!-- Mode selection buttons -->
            <div class="flex flex-col sm:flex-row gap-4 sm:gap-6">
              <!-- Emit 'select' event with 'user' for User Mode -->
              <button
                @click="$emit('select', 'user')"
                class="flex-1 py-3 sm:py-4 bg-blue-500 text-white rounded-lg shadow-lg hover:shadow-xl transform hover:-translate-y-0.5 transition"
              >
                User Mode
              </button>

              <!-- Emit 'select' event with 'guest' for Guest Mode -->
              <button
                @click="$emit('select', 'guest')"
                class="flex-1 py-3 sm:py-4 bg-green-500 text-white rounded-lg shadow-lg hover:shadow-xl transform hover:-translate-y-0.5 transition"
              >
                Guest Mode
              </button>
            </div>
          </div>

          <!-- Right side: Illustration -->
          <div class="flex justify-center md:justify-end">
            <img
              src="@/assets/todolist.png"
              alt="To-Do Illustration"
              class="w-48 sm:w-56 md:w-64 h-auto rounded-lg border border-gray-200 shadow-2xl"
            />
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'ModeSelector',
  // This component emits a 'select' event with the chosen mode ('user' or 'guest')
  emits: ['select']
}
</script>

<style>
/* No additional styles needed; all done via Tailwind CSS utilities */
</style>
