<template>
  <div id="container">
    <div id="register" class="text-center">
      <v-form v-on:submit.prevent="register">
        <h1>Create Account</h1>
        <br>
        <div role="alert" v-if="registrationErrors">
          {{ registrationErrorMsg }}
          <br>
          <br>
        </div>
        <div class="form-input-group">
          <label for="first-name"></label>
          <v-text-field label="First Name" variant="underlined" type="text" id="firstname" v-model="user.firstname" required autofocus />
        </div>
        <div class="form-input-group">
          <label for="last-name"></label>
          <v-text-field label="Last Name" variant="underlined" type="text" id="lastname" v-model="user.lastname" required autofocus />
        </div>
        <div class="form-input-group">
          <label for="email"></label>
          <v-text-field label="Email" variant="underlined" type="text" id="email"  v-model="user.email" required autofocus />
        </div>
        <div class="form-input-group">
          <label for="password"></label>
          <v-text-field label="Password" variant="underlined" type="password"  id="password" v-model="user.password" required />
        </div>
        <div class="form-input-group">
          <label for="confirmPassword"></label>
          <v-text-field label="Confirm Password" variant="underlined" type="password" id="confirmPassword" v-model="user.confirmPassword"
            required /> 
          </div>
          
          <v-btn elevation="12" rounded="xl" size="regular" type="submit">CREATE ACCOUNT</v-btn>
          <br>
        <br>
        <p><router-link v-bind:to="{ name: 'login' }">Already have an account? Log in.</router-link></p>
      </v-form>
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
  margin-bottom: 20px;

}

.text-center {
  display: flex;
  width: 350px;
  height: 640px;
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

button{
  width: 200px;
}



</style>
