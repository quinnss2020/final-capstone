<template>
  <div id="confirm-email">
    <form v-on:submit.prevent="confirmEmail">
      <h1 >Confirm Email View</h1>
      <div role="alert" v-if="invalidCredentials">
        Invalid code!
      </div>
      <div class="form-input-group">
        <label for="code">Code</label>
        <input type="text" id="code" v-model="code" required autofocus />
      </div>
      <button type="submit">Confirm</button>
      <!-- <p><router-link v-bind:to="{ name: 'register' }">Need another code? Click Here.</router-link></p> -->
    </form>
  </div>
</template>

<script>
import userService from "../services/UserService";

export default {
  components: {},
  data() {
    return {
      user: {
        email: "",
        password: ""
      },
      invalidCredentials: false,
      code: '',
    };
  },
  methods: {
    confirmEmail() {
      userService
        .confirmEmail(this.user, this.code)
        .then(response => {
          if (response.status == 200) {
            this.$store.commit("SET_AUTH_TOKEN", response.data.token);
            this.$store.commit("SET_USER", response.data.user);
            this.$router.push("/");
          }
        })
        .catch(error => {
          const response = error.response;

          if (response.status === 401) {
            this.invalidCredentials = true;
          }
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