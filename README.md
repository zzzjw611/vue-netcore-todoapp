# Vue + .NET Core To-Do Application

A two-mode (Guest/User) to-do list built with Vue 3, Tailwind CSS on the front end, and C# .NET Core + Entity Framework Core on the back end. Guest mode stores up to five items in browser memory; User mode persists unlimited items in a SQLite database via a factory pattern.

## Approach

I build the backend by defining an ITodoProvider interface and then made two providers—one that keeps things in memory (for guests) and one that talks to SQLite via Entity Framework Core (for users). A simple TodoProviderFactory reads an X-Provider-Mode header and hands off whichever provider we need. On the frontend, I built a single‐page Vue 3 app that first asks “Guest or User?” and then wires up either a tiny in-memory class or a set of Axios calls to our API. Styling was super quick with the Tailwind CDN—complete with a “glassmorphism” welcome card—and I made sure it works on all screen sizes. I also added a debounced search so the server doesn’t get hammered, plus client-side filters and clean separation between ModeSelector.vue, TodoApp.vue, and TodoItem.vue.

## Challenges

Honestly, the trickiest bit was getting Tailwind to behave under Vite—my initial PostCSS setup kept stripping out hover and transition classes, so I resorted to dropping the official CDN script into index.html to make everything work. I also spent way too long on the layout: I started with a rigid two-column Flexbox that left a blank pane on one side, and ultimately fixed it by swapping between the selector and the to-do app using a simple v-if. Lastly, implementing the in-memory provider’s “max 5 items” rule forced me to handle list immutability carefully so I could drop the oldest task without unwanted side-effects.

## Future Improment

If time allows I’d build a user profile and authentication flow so returning users can skip the mode selector and dive straight into “User” mode with their saved todos; Also I’d continue polishing the front-end with micro-interactions and smoother animations to make the app feel more alive; and I’d introduce contextual prompts—like a banner when a guest hits the five-item limit—highlighting the benefits of persistent storage to attract guests toward registration and capture more potential users.

## Resources

- [Vue 3 Composition API](https://v3.vuejs.org/guide/composition-api-introduction.html)  
- [Tailwind CSS CDN Quickstart](https://tailwindcss.com/docs/installation/play-cdn)  
- [Axios Docs](https://axios-http.com/docs/intro)  
- [ASP.NET Core Minimal APIs](https://docs.microsoft.com/aspnet/core/fundamentals/minimal-apis)  
- [Entity Framework Core Migrations](https://docs.microsoft.com/ef/core/managing-schemas/migrations/)
