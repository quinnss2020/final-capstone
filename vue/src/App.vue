<template>
  <div id="capstone-app">
    <navigation/>
    <!-- <div id="nav">
      <router-link v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'logout' }" v-if="this.$store.state.token != ''">Logout</router-link>
    </div> -->
    <router-view />
  </div>
</template>

<script>
import Navigation from "./components/Navigation.vue"

export default {
  name: 'App',
  components: {Navigation},

}

</script>

<style>
@import url('https://fonts.googleapis.com/css2?family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap');
#app {
  font-family: "Roboto", sans-serif;
  text-align: center;
}
</style>
