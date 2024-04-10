<template>
  <div class="home">
    <h1 v-if="$store.state.token"> Welcome {{$store.state.user.currentUser}}</h1>
    <!-- <p>You must be authenticated to see this</p> -->
  </div>
</template>

<script>
import { stringifyQuery } from 'vue-router';


export default {
  props: ['firstName', 'lastName'],
}

</script>

<style scoped>
h1 {
-webkit-text-fill-color:#264B56;
-webkit-text-stroke: 1.5px black;
font-weight: 900;
font-size: 40px;

}

p {
  color: #141616;
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
