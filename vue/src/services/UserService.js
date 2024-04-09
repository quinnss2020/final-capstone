import axios from 'axios';

export default {

  login(user) {
    return axios.post('/login', user)
  },

  register(user) {
    return axios.post('/register', user)
  },

  confirmEmail(user, code) {
    return axios.post('/login/confirm', {user:user, code:code})
  }


}
