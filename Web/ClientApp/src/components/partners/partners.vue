<template>
    <div class="container partner-container" v-if="partners.length > 0">
        <div class="col-md-12 text-center">
            <h6>Nossos Parceiros</h6>
        </div>
        <div class="col-md-12">
            <splide :options="sliderOptions" has-slider-wrapper=has-slider-wrapper>
                <splide-slide v-for="partner in partners" :key="partner.id">
                    <a :href="partner.website" target="_blank" :title="partner.name">
                        <img :src="partner.logoUrl" alt="partner.name" width="250" height="150" />
                    </a>
                </splide-slide>
            </splide>
        </div>
    </div>
</template>
<script lang="ts" src="./partners.ts">
</script>
<style scoped type="text/sass">
    .partner-container {
        margin-top: 20px;
    }
    h6 {
        font-size: 2.1875rem;
        font-weight: 700;
    }
</style>