<template>
  <div id="container">
    <div id="register" class="text-center">
      <form v-on:submit.prevent="register">
        <h1>Create Account</h1>
        <br>
        <div role="alert" v-if="registrationErrors">
          {{ registrationErrorMsg }}
          <br>
          <br>
        </div>
        <div class="form-input-group">
          <label for="first-name"></label>
          <input type="text" id="firstname" placeholder="First Name" v-model="user.firstname" required autofocus />
        </div>
        <div class="form-input-group">
          <label for="last-name"></label>
          <input type="text" id="lastname" placeholder="Last Name" v-model="user.lastname" required autofocus />
        </div>
        <div class="form-input-group">
          <label for="email"></label>
          <input type="text" id="email" placeholder="Email" v-model="user.email" required autofocus />
        </div>
        <div class="form-input-group">
          <label for="password"></label>
          <input type="password" placeholder="Password" id="password" v-model="user.password" required />
        </div>
        <div class="form-input-group">
          <label for="confirmPassword"></label>
          <input type="password" placeholder="Confirm Password" id="confirmPassword" v-model="user.confirmPassword"
            required /> 
          </div>
          <br>
          <button type="submit">CREATE ACCOUNT</button>
        <br>
        <p><router-link v-bind:to="{ name: 'login' }">Already have an account? Log in.</router-link></p>
      </form>
    </div>
  </div>
</template>

<script>
import userService from '../services/UserService';

export default {
  data() {
    return {
      user: {
        firstname: '',
        lastname: '',
        email: '',
        password: '',
        confirmPassword: '',
        role: 'user',
        code: '',
      },
      registrationErrors: false,
      registrationErrorMsg: 'There were problems registering this user.',
    };
  },
  methods: {
    register() {
      if (this.user.password != this.user.confirmPassword) {
        this.registrationErrors = true;
        this.registrationErrorMsg = 'Password & Confirm Password do not match.';
      } else {
        userService
          .register(this.user)
          .then(() => {

            this.$router.push({
              path: '/login',
              query: { registration: 'success' },
            });
          }
          )
          .catch((error) => {
            const response = error.response;
            this.registrationErrors = true;
            if (response.status === 400) {
              this.registrationErrorMsg = 'Bad Request: Validation Errors';
            }
            else if (response.status === 409) {
              this.registrationErrorMsg = 'Email already registered. Please log in.';
            }
          });
      }
    },
    clearErrors() {
      this.registrationErrors = false;
      this.registrationErrorMsg = 'There were problems registering this user.';
    },
  },
};
</script>

<style scoped>
.form-input-group {
  margin-bottom: 1rem;

}

/* label {
  /* margin-right: 0.5rem; 
}*/


#container {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 100%;
  margin-top: 50px;

}

.text-center {
  display: flex;
  width: 350px;
  height: 500px;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  border-radius: 2rem;
  border-width: 2px;
  margin-top: 20px;
  /* background-color: #F9F6F0; */
  border-style: outset;
  border-color: #F4ECE1;
  margin-bottom: 20px;
  background: rgba(244, 236, 225, .5);

}

div[role=alert] {
  color: red;
  font-weight: 500;

}

h1 {
  font-size: 1.5rem;
}

a {
  font-weight: 750;
  color: #314668;
}

</style>
