import { Component, Vue } from 'vue-property-decorator';
import teamService from '@/services/team.service';
import Professional from '@/models/professional.model';

@Component
export default class Equipe extends Vue {
  private teamMembers: Professional[] = [];

  private mounted() {
    this.fetchData();
  }

  private async fetchData() {
    this.teamMembers = await teamService.getMembers();
  }
}
