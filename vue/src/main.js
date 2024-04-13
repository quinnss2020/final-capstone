import { createApp } from 'vue'
import CapstoneApp from './App.vue'
import { createStore } from './store'
import router from './router'
import axios from 'axios'
import {v2 as cloudinary} from 'cloudinary';
  
//these are our cloudinary credentials
cloudinary.config({ 
  cloud_name: 'dpicdmebf', 
  api_key: '434166887691892', 
  api_secret: 'Lfui7OUC0GzisU5ktSi7a5dm2FA' 
});

/* sets the base url for server API communication with axios */
axios.defaults.baseURL = import.meta.env.VITE_REMOTE_API;

/*
 * The authorization header is set for axios when you login but what happens when 
 * you come back or the page is refreshed. When that happens you need to check 
 * for the token in local storage and if it exists you should set the header 
 * so that it will be attached to each request.
 */
let currentToken = localStorage.getItem('token');
let currentUser = JSON.parse(localStorage.getItem('user'));

if (currentToken) {
  // Set token axios requests
  axios.defaults.headers.common['Authorization'] = `Bearer ${currentToken}`;
}

// Create the Vuex store passing in the stored credentials
const store = createStore(currentToken, currentUser);

const app = createApp(CapstoneApp);
app.use(store);
app.use(router);
app.mount('#app');