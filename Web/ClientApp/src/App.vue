<template>
  <div id="app">
    <router-view />
  </div>
</template>

<style src="./styles/main.scss" lang="scss"></style>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component
export default class App extends Vue {}
</script>
