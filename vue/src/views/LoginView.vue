<template>
  <div id="login">
    <form v-on:submit.prevent="login">
      <h1 >Please Sign In</h1>
      <div role="alert" v-if="invalidCredentials">
        Invalid email and password!
      </div>
      <div role="alert" v-if="this.$route.query.registration">
        Thank you for registering, please sign in.
      </div>
      <div class="form-input-group">
        <label for="email">Email</label>
        <input type="text" id="email" v-model="user.email" required autofocus />
      </div>
      <div class="form-input-group">
        <label for="password">Password</label>
        <input type="password" id="password" v-model="user.password" required />
      </div>
      <button type="submit" v-show="!isConfirmed">Sign in</button>
    </form>
    <form v-if="isConfirmed" v-on:submit.prevent="confirm">
      <div class="form-input-group">
        <p> A confirmation email was sent to you, please enter the code below.</p>
        <label for="code">Email Confirmation Code</label>
        <input type="text" id="code" v-model="code" required autofocus />
      </div>
      <button type="submit">Confirm</button>
      </form>
      <p>
        <router-link v-bind:to="{ name: 'register' }">Need an account? Sign up.</router-link>
      </p>
  </div>
</template>

<script>
import userService from "../services/UserService"; //user
import authService from "../services/AuthService"; //admin

export default {
  components: {},
  data() {
    return {
      user: {
        email: "",
        password: "",
      },
      invalidCredentials: false,
      isConfirmed: false,
      code: '',
    };
  },
  methods: {
    login() {
      userService
        .login(this.user)
        .then(response => {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
          if(response.status == 200)
          {
            this.isConfirmed = true;
          }
          else {
            this.$router.push("/login/terms");
          }
        })
        .catch(error => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
        });
    },
    confirm() {
      userService
        .confirmEmail(this.user, this.code)
        .then(response => {
          if(response.status == 200) {
            this.$router.push("/login/terms");
          }
        })
        .catch(error => {
          const response = error.response;
          this.invalidCredentials = true;         
        });

    }
  
  }
};
</script>

<style scoped>
.form-input-group {
  margin-bottom: 1rem;
}
label {
  margin-right: 0.5rem;
}
</style>