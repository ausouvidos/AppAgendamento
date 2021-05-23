import { Component, Vue } from 'vue-property-decorator';
import CalendarPsicologo from '@/components/calendar-psicologo/calendar-psicologo.vue';
import { TheMask } from 'vue-the-mask';
import { Table, TableColumn, Cascader } from 'element-ui';
import companyService from '@/services/company.service';
import Company from '@/models/company.model';
import teamService from '@/services/team.service';
import Professional from '@/models/professional.model';
import userService from '@/services/user.service';
import User from '@/models/user.model';
import { ICascader, ICascaderChildren } from '@/interfaces/cascader.interface';
import { IProfessionalsParser } from '@/interfaces/professionals-parser.interface';

@Component({
  components: {
    CalendarPsicologo,
    'el-table': Table,
    TableColumn,
    TheMask,
    'el-cascader': Cascader,
  },
})
export default class EmpresasPendenteAprovacao extends Vue {
  private companies: Company[] = [];
  private selectedCompany: Company = new Company();
  private estados = ['AC', 'AL', 'AP', 'AM', 'BA', 'CE', 'DF', 'ES', 'GO', 'MA', 'MT', 'MS', 'MG', 'PA', 'PB', 'PR', 'PE', 'PI', 'RJ', 'RN', 'RS', 'RO', 'RR', 'SC', 'SP', 'SE', 'TO'];
  private quantidade: number = 0;
  private professionals: ICascader[] = [];
  private selectedProfessionals: string[] = [];

  private async mounted() {
    await this.loadData();
  }

  private async loadData() {
      this.companies = await companyService.getPendingApproval();

      const users: User[] = await userService.getUsers();
      this.professionals = users.map((user: User) => ({
          value: user.id,
          label: `${user.name} - ${user.email}`,
      } as ICascader));
  }

  private openItem(item: Company, cell: TableColumn, event: any) {
    this.selectedCompany = item;
    (this.$refs['aprovar-empresa'] as any).show();
  }

  private async aprovar() {
    await companyService.approve(this.selectedCompany, this.quantidade, this.selectedProfessionals);
    await this.loadData();
  }

  private handleProfessionalsChange(values: any[]) {
    this.selectedProfessionals = values.map((item: any[]) => item[0]);
  }
}
