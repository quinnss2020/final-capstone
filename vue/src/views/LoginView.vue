<template id="template">
  <div id="login-container">
    <div id="login">
    <form v-on:submit.prevent="login">
      <br>
      <h1>Please Sign In</h1>
      <br>
      <br>

      <div role="alert" v-if="invalidCredentials">
        Invalid email and password!
      </div>
      <div role="alert" v-if="this.$route.query.registration">
        Thank you for registering, please sign in.
      </div>
      <div class="form-input-group">
      
        <input type="text" placeholder="Email" id="email" v-model="user.email" required autofocus />
      </div>
      <div class="form-input-group">
        
        <input type="password" placeholder="Password" id="password" v-model="user.password" required />
      </div>
      
      <br>
      <button type="submit" v-show="!isConfirmed">SIGN IN</button>
      <!-- <button v-bind:to="{ name: 'register' }">SIGN UP</button> -->
      <br>
    </form>
    <form v-if="isConfirmed" v-on:submit.prevent="confirm">
      <div class="form-input-group">
        <p> A confirmation email was sent to you, please enter the code below.</p>
     
        <input type="text" placeholder="Email Confirmation Code" id="code" v-model="code" required autofocus />
      </div>
      <button type="submit">CONFIRM</button>
      </form>
      <p>
      <br>
      <router-link v-bind:to="{ name: 'register' }">Need an account? Sign up.</router-link>
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
#login-container{
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 100%;
  margin-top: 200px
}

#login {
display: flex;
width: 300px;
flex-direction: column;
justify-content: center;
align-items: center;
border-radius: 2rem;
margin-top: 20px;
background-color:#B4B09B;
padding: 1.5rem;
}
.form-input-group {
  margin-bottom: 1rem;
  text-align: left;
  
}
label {
  margin-right: 0.5rem;
  font-family: "Roboto", sans-serif;
  font-weight: 500;
  color: #264B56;
}

h1 {
  font-size: 1.5rem;
  color: #264B56
}

input {
  align-items: right;
  background-color: #FAEFE0;
  border: none;
  border-radius: 0.3rem;
  padding: 8px;
 
}

p{
  font-weight: 500;
  color:#264B56;
  text-decoration: none;
  font-size: 14px;
  
}

/* button {
  margin-top: 10px;
} */


</style>