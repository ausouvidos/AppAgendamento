import { Component, Vue } from 'vue-property-decorator';
import teamService from '@/services/team.service';
import Professional from '@/models/professional.model';
import EquipeItem from './equipe-item.vue';

@Component({
  components: {
    'equipe-item': EquipeItem,
  },
})
export default class Equipe extends Vue {
  private teamMembers: Professional[] = [];
  private creators: Professional[] = [];
  private supporters: Professional[] = [];

  private mounted() {
    this.fetchData();
  }

  private async fetchData() {
    const members = await teamService.getMembers();
    this.teamMembers = members.filter((a) => a.grupo === 'Equipe');
    this.creators = members.filter((a) => a.grupo === 'Idealizadores');
    this.supporters = members.filter((a) => a.grupo === 'Apoiadores');
  }
}
