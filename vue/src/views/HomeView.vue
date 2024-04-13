<template>
  <div class="home">
    <h1 v-if="$store.state.token"> Welcome {{$store.state.user.currentUser}}</h1>
    <p>You are now logged in</p>
  </div>
</template>

<script>
import { stringifyQuery } from 'vue-router';


export default {
  props: ['firstName', 'lastName'],
}

</script>

<style scoped>

template {
  background-color:#B4B09B;
}

h1 {
color: #faefe0;
font-style: italic;
font-weight: 800;
font-size: 3rem;

}

p {
  color: #faefe0;;
  font-weight: bold;
}

.home {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  margin-top: 100px;
}
</style>
