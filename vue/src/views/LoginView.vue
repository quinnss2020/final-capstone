<template id="template">
  <div id="login-container">
    <div id="login">
      <form v-on:submit.prevent="login">
        <br>
        <h1>Please Sign In</h1>
        <br>

        <div id="alert" role="alert" v-if="invalidCredentials">
          Invalid email and password!
        </div>
        <div role="alert" v-if="this.$route.query.registration">
          Thank you for registering, please sign in.
        </div>
        <div class="form-input-group">

          <v-text-field label="Email" variant="underlined" type="text" id="email" v-model="user.email" required autofocus></v-text-field>
        </div>
        <div class="form-input-group">

          <v-text-field type="password" variant="underlined" label="Password" id="password" v-model="user.password" required></v-text-field>
        </div>
        <v-btn elevation="8" rounded="xl" size="regular" :ripple="true" type="submit" v-show="!isConfirmed">SIGN IN</v-btn>
      </form>
      <v-btn elevation="8" rounded="xl" size="regular" :ripple="true" id="new-user" v-show="!isConfirmed" v-on:click="this.$router.push('/register')">NEW USER</v-btn>
      
      <form v-if="isConfirmed" v-on:submit.prevent="confirm">
        <div class="form-input-group">
          <p> A confirmation email was sent to you, please enter the code below.</p>
          <br>
          <v-text-field type="text" variant="underlined" label="Email Confirmation Code" id="code" v-model="code" required autofocus></v-text-field>
        </div>
        <v-btn elevation="12" rounded="xl" size="regular" type="submit">CONFIRM</v-btn>
      </form>
      <br>
      <p>
        
        <!-- <router-link v-bind:to="{ name: 'register' }">Need an account? Sign up.</router-link> -->
      </p>
    </div>
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
          if (response.status == 200) {
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
          if (response.status == 200) {
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
#login-container {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 100%;
  margin-top: 30px
}


#login {
  display: flex;
  width: 300px;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  border-radius: 2rem;
  border-width: 2px;
  margin-top: 20px;
  /* background-color: #F4ECE1; */
  background: rgba(244, 236, 225, .5);
  border-style: outset;
  border-color: #F4ECE1;
  padding: 0.5rem;

}

#alert {
  color:red;
  padding-bottom: 5px;
}

.form-input-group {
  margin-bottom: 1rem;

}

label {
  margin-right: 0.5rem;
  font-weight: 500;
  color:#314668;
}

h1 {
  font-size: 1.5rem;
  color:#314668;
}

/* input {

  background-color: #FAECE1; 
  border: none;
  border-radius: 0.3rem;
  padding: 8px;

} */

p {
  font-weight: 500;
  color: #314668;
  text-decoration: none;
  font-size: 14px;

}

button {
  margin-top: 10px;
}

#new-user{
  background-color:#314668;
  color:#F9F6F0; 
}
</style>