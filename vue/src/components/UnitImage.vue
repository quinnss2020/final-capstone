<template>
    <div class="dropzone" @dragover.prevent @dragenter.prevent @dragstart.prevent @drop.prevent="handleFileChange($event.dataTransfer)">
        <input id="file-input" type="file" accept="image/png, image/jpeg" v-on:change="handleFileChange($event.target)" required />
        <h2 for="file-input">Click or Drag and Drop Image</h2>
        <img v-if="preview" v-bind:src="preview">
        <h3 v-if="preview">File name: {{ fileName }} </h3>
    </div>

    <button type="submit" v-on:click="upload">Upload</button>
    <h3 v-if="success">File Uploaded Successfully. publicId: {{success}}</h3>
</template>

<script>
export default {
  name: "TestUploadImage",
  data() {
    return { 
      test: false,
      fileName: "",
      preview: null,
      preset: import.meta.env.VITE_APP_UPLOAD_PRESET,
      formData: null,
      cloudName: import.meta.env.VITE_APP_CLOUD_NAME,
      success: "",
    };
  },
  methods: {
    handleFileChange: function (event) {
      this.file = event.files[0];
      this.fileName = this.file.name;
      
      this.formData = new FormData();
      this.formData.append("upload_preset", this.preset);

      let reader = new FileReader();
      reader.readAsDataURL(this.file);

      reader.onload = (e) => {
        this.preview = e.target.result;
        this.formData.append("file", this.preview);
      }
    },
    upload: async function() {
      const res = await fetch(
        `https://api.cloudinary.com/v1_1/${this.cloudName}/image/upload`,
          {
            method: "POST",
            body: this.formData,
          }
      );
      const data = await res.json();
      console.log(data.secure_url);
      this.fileName = "";
      this.preview = null;
      this.formData = null;
      this.success = data.public_id;
    },
  }
}
</script>

<style scoped>
.dropzone {
  height: fit-content;
  min-height: 200px;
  max-height: 400px;
  width: 600px;
  background: #fdfdfd;
  border-radius: 2rem;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  margin: 0 auto;
}
input[type="file"] {
  position: absolute;
  opacity: 0;
  width: inherit;
  min-height: 200px;
  max-height: 400px;
  cursor: pointer;
}

img {
  max-height: 300px;
  max-width: 600px;
  width: 50%;
  height: 50%;
}
</style>