<template>
    <div>
        {{ currentTime }}
        <slot></slot>
    </div>
</template>

<script>
import { reformatDate } from '../helpers/countdown';

export default {
    name: "Countdown",
    props: ['expiration'],
    data () {
        return {
            currentTime: "",
            isExpired: reformatDate(this.expiration),
        }
    },

    mounted () {
        setInterval(() => this.currentTime = reformatDate(this.expiration), 1000);
    },
}

</script>