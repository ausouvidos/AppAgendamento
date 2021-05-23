import { Component, Vue } from 'vue-property-decorator';
import partnerService from '@/services/partner.service';
import Partner from '@/models/partner.model';
import { Splide, SplideSlide } from '@splidejs/vue-splide';

@Component({
    components: {
        Splide,
        SplideSlide,
    },
})
export default class Partners extends Vue {
    private partners: Partner[] = [];
    private sliderOptions = {
        rewind: true,
        autoplay: true,
        gap: '1rem',
        pauseOnHover: false,
        arrows: 'slider',
        perPage: 4,
        breakpoints: {
            640: {
                perPage: 2,
            },
            768: {
                perPage: 2,
            },
        },
        fixedWidth: 250,
        fixedHeight: 150,
    };

    private mounted() {
        this.fetchData();
    }

    private async fetchData() {
        this.partners = await partnerService.getPartners();
    }
}
