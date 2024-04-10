<template>
    <div id="view-units">
        <h1>This is the Unit View</h1>
        <section class="unit-container">
            <unit v-for="unit in units" v-bind:key="unit.id" v-bind:item="unit"/>
        </section>
    </div>
</template>

<script>
import Unit from '../components/Unit.vue';

export default {
    props: [],
    components: {
        Unit,
    },

    data () {
        return {
            units: [],

        }
    }
    
}

</script>

<style scoped> </style>