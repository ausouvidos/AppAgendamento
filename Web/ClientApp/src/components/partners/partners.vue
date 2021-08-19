<template>
    <div class="container partner-container" v-if="partners.length > 0">
        <div class="col-md-12 text-center">
            <h6>Nossos Parceiros</h6>
        </div>
        <div class="col-md-12">
            <splide :options="sliderOptions" has-slider-wrapper=has-slider-wrapper>
                <splide-slide v-for="partner in partners" :key="partner.id" class="partner-slide">
                    <a :href="partner.website" target="_blank" :title="partner.name">
                        <img :src="partner.logoUrl" :alt="partner.name" />
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
    .partner-slide {
        overflow: hidden;
        width: 230px;
        height: 150px;
        background-color: white;
        display: flex;
        justify-content: center;
        align-content: center;
        align-items: center;
    }
        .partner-slide img {
            max-height: 150px;
            max-width: 100%;
            object-fit: cover;
        }
</style>