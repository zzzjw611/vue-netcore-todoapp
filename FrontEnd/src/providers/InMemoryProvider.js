// src/providers/InMemoryProvider.js
export default class InMemoryProvider {
    constructor() {
      this.todos = []
      this.idCounter = 1
      this.maxItems = 5   // up to 5 items
    }
  
    async getTodos(search = '') {
      let list = [...this.todos]
      if (search) {
        const key = search.toLowerCase()
        list = list.filter(t => t.title.toLowerCase().includes(key))
      }
      return list
    }
  
    async addTodo({ title, completed = false }) {
      if (this.todos.length >= this.maxItems) {
        this.todos.shift()  // delete if exceeds max items
        console.warn('Exceeded max items, deleting the oldest one.')
      }
      const todo = { id: this.idCounter++, title, completed }
      this.todos.push(todo)
      return todo
    }
  
    async updateTodo(id, updates) {
      const idx = this.todos.findIndex(t => t.id === id)
      if (idx > -1) {
        this.todos[idx] = { ...this.todos[idx], ...updates }
        return this.todos[idx]
      }
      return null
    }
  
    async deleteTodo(id) {
      const idx = this.todos.findIndex(t => t.id === id)
      if (idx > -1) {
        this.todos.splice(idx, 1)
      }
      return id
    }
  }
  