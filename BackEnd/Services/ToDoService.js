// src/services/todoService.js
import axios from 'axios';

// Create a reusable Axios instance, pointing at backend API
const api = axios.create({
  baseURL: 'http://localhost:5000/api/todo'
});
// set the mode
export function setProviderMode(mode) {
  api.defaults.headers['X-Provider-Mode'] = mode;
}
// fetch the list of todos
export function getTodos(search = '') {
  return api.get('', { params: { search } }).then(res => res.data);
}
// create a new todo item.
export function addTodo(todo) {
  return api.post('', todo).then(res => res.data);
}
// update an existing todo.
export function updateTodo(id, todo) {
  return api.put(`/${id}`, todo).then(res => res.data);
}
// delete a todo item.
export function deleteTodo(id) {
  return api.delete(`/${id}`);
}
