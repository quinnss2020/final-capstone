<template>
    <div id="view-units">
        <h1>This is the Unit View</h1>
        <section class="unit-container">
            <unit v-for="unit in units" v-bind:key="unit.id" v-bind:item="unit" />
        </section>
    </div>
</template>

<script>
// import Unit from '../components/Unit.vue';
import UnitService from '../services/UnitService';

export default {
    props: [],
    components: {
      
    },

    data() {
        return {
            units: [],

        }
    },

    methods: {
        getUnits(){
            UnitService
            .list()
            .then((response) => {
                this.units = response.data;
            })
            .catch((error) => {
                if (error.response) {
                    console.log("Error loading units: ", error.response.status);
                }
                else if (error.request) {
                    console.log("Error loading units. Unable to communicate to server.");
                }
                else {
                    console.log("Error making request");
                }
            })
        }
    },

    created() {
        UnitService
        .list()
        .then((response) => {
            this.units = response.data;
            
        })
        .catch(); //TODO add generic error handling

        this.getUnits();
    },


}

</script>

<style scoped></style>