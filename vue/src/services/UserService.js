import axios from 'axios';

export default {

  login(user) {
    return axios.post('/login', user)
  },

  logout() {
   return axios.put('/logout')
  },

  register(user) {
    return axios.post('/register', user)
  },

  confirmEmail(user, code) {
    return axios.put('/login/confirm', {user:user, code:code})
  },

  agreeTerms(user, agreed)
  {
    return axios.put('/login/terms', {user: user, agreed:agreed})
  }


}
