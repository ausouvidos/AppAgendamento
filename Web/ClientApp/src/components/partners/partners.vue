<template>
    <div class="container partner-container" v-if="partners.length > 0">
        <div class="col-md-12 text-center">
            <h6>Nossos Parceiros</h6>
        </div>
        <div class="col-md-12">
            <splide :options="sliderOptions" has-slider-wrapper=has-slider-wrapper>
                <splide-slide v-for="partner in partners" :key="partner.id" style="overflow: hidden; width: 230px; height: 150px">
                    <a :href="partner.website" target="_blank" :title="partner.name">
                        <img :src="partner.logoUrl" :alt="partner.name" style="height: 150px; width: 100%; object-fit: cover;" />
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