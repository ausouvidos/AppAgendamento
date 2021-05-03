import { Component, Vue } from 'vue-property-decorator';
import CalendarPsicologo from '@/components/calendar-psicologo/calendar-psicologo.vue';
import { TheMask } from 'vue-the-mask';
import { Table, TableColumn, Cascader } from 'element-ui';
import companyService from '@/services/company.service';
import Company from '@/models/company.model';
import teamService from '@/services/team.service';
import Professional from '../models/professional.model';

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
  private professionals = [];
    

  private async mounted() {
    await this.loadData();
  }

  private async loadData() {
      this.companies = await companyService.getPendingApproval();
      //const professionals = ((await teamService.getMembers())?.filter((a) => a.grupo === 'Equipe') || [])
      //    .reduce((p, c) => {
      //        if (p.hasOwnProperty(c.funcao)) {
      //            p[c.funcao].push(c);
      //        }
      //        else {
      //            p[c.funcao] = [c];
      //        }
      //        return p;
      //    }, {});

      //this.professionals = Object.keys(professionals).map((key: string) => ({
      //    value: key,
      //    label: key,
      //    children: professionals[key]
      //        .map((p: Professional) => ({ value: p.id, label: p.nomeCompleto }))
      //        .sort((a, b) => a.label.localeCompare(b.label))
      //}));
      
  }

  private openItem(item: Company, cell: TableColumn, event: any) {
    this.selectedCompany = item;
    (this.$refs['aprovar-empresa'] as any).show();
  }

  private async aprovar() {
    await companyService.approve(this.selectedCompany, this.quantidade);
    await this.loadData();
  }

  private handleProfessionalsChange(value: any) {
    console.log(value)
  }
}
