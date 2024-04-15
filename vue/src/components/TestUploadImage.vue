<template>
    <button type="button" v-on:click="hitMethod, open" > Upload Photo </button>
    <input id="file-input" type="file" accept="image" />
</template>

<script>
import { useFileDialog } from '@vueuse/core'

export default {
  methods: {
    hitMethod(){
      console.log("file method hit");
    }
  }
}

const { files, open, reset, onChange } = useFileDialog({
  accept: 'image/*', // Set to accept only image files
  directory: true, // Select directories instead of files if set true
})

onChange((files) => {
  /** do something with files */
})
</script>

<style>
</style>