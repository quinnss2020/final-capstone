<template>
    <div class="unit-details">
        <h1>{{ item.city }} {{ item.id }}</h1>

    </div>
</template>

<script>

export default {
    name: 'unit',
    props: ['item']
}

</script>

<style>
</style>