<template>
  <div class="banner">
    <img class="banner-image" src="@/assets/banner-home.jpg" alt="">
    <h1 class="banner-title">
      <span class="line-1">Aus Ouvidos</span>
      <span class="line-2">Uma ousadia na escuta</span>
    </h1>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator';

@Component
export default class Banner extends Vue {}
</script>
