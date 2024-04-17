<template>
</template>

<script>
import PhotoService from "../services/PhotoService";

export default {
  data() {
    return {
      photos: [],
      unitId: 1001,
      photo1: {}
    };
  },

  methods: {
  },

  created() {
    this.photo1 = this.photos.at(0);
  }
};
</script>

<style scoped>
#image{
  display: flex;
}
.v-carousel__controls__item.v-btn.v-btn--icon {
    background-color: #ebece9; /* Background color of non-active ones */
    height: 2px; /* Height you want */
    width: 40px; /* Width you want */
    border-radius: 0; /* Remove default border radius */
}
  
.v-carousel__controls__item.v-btn.v-btn--icon.v-btn--active {
    background-color: #1f1f1f; /* Colour for active one */
}
</style>
