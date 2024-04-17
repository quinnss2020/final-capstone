<template>
  <v-container>
    <v-carousel :show-arrows="false" >
      <v-carousel-item  id="image" v-for="(photo,id) in photos" :key="id"
        :src="photo.url"
        
      ></v-carousel-item>
    </v-carousel>
  </v-container>
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
    getPhotoList() {
      PhotoService
      .getPhotos(this.unitId)
        .then((response) => {
          this.photos = response.data;
        })
        .catch((error) => {
          if (error.response) {
            console.log("Error loading photos: ", error.response.status);
          } else if (error.request) {
            console.log(
              "Error loading photos. Unable to communicate to server."
            );
          } else {
            console.log("Error making request");
          }
        });
    },
  },

  created() {
    this.getPhotoList();
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
