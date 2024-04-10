<template>
    <div>
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
    </div>
</template>

<script>
</script>

<style>
</style>