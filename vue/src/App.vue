<template>
  <div id="capstone-app">
    <navigation />
    <!-- <div id="nav">
      <router-link v-bind:to="{ name: 'home' }">Home</router-link>&nbsp;|&nbsp;
      <router-link v-bind:to="{ name: 'logout' }" v-if="this.$store.state.token != ''">Logout</router-link>
    </div> -->
    <router-view />
  </div>
</template>

<script>
import Navigation from "./components/Navigation.vue"
import TestUploadImage from "./components/TestUploadImage.vue"

export default {
  name: 'App',
  components: { Navigation },

}

</script>

<style>
@import url('https://fonts.googleapis.com/css2?family=Roboto:ital,wght@0,100;0,300;0,400;0,500;0,700;0,900;1,100;1,300;1,400;1,500;1,700;1,900&display=swap');

html {
  font-family: "Roboto", sans-serif;
  text-align: center;
  color:#faefe0;
  background-color: #677477;
  margin: 0;
  height: 100vh;
  width: 100vw;
  padding: 0;
  top: 0;
  bottom: 0;
  position: absolute;
}

button {
  font-family: "Roboto", sans-serif;
  font-weight: 500;
  color: #264B56;
  background-color: #faefe0;
  border-color: #264B56;
  border-radius: 0.6rem;
  padding: 8px;
  width: 170px;
  height: 35px;
  margin-bottom: 7px;
  white-space: nowrap;
}


input {
background-color: #FAEFE0;
border: none;
border-radius: 0.3rem;
padding: 8px;
}
</style>
