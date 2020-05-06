import { Component, Vue } from 'vue-property-decorator';
import teamService from '@/services/team.service';
import Professional from '@/models/professional.model';

@Component
export default class Equipe extends Vue {
  private teamMembers: Professional[] = [];
  private idealizadores: Professional[] = [];

  private mounted() {
    this.fetchData();
  }

  private async fetchData() {
    const members = await teamService.getMembers();
    this.teamMembers = members.filter((a) => !a.idealizador);
    this.idealizadores = members.filter((a) => a.idealizador);
  }
}
