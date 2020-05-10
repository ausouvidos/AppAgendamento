import { Component, Vue } from 'vue-property-decorator';
import CalendarPsicologo from '@/components/calendar-psicologo/calendar-psicologo.vue';
import { TheMask } from 'vue-the-mask';
import { Table, TableColumn } from 'element-ui';
import companyService from '@/services/company.service';
import Company from '@/models/company.model';

@Component({
  components: {
    CalendarPsicologo,
    'el-table': Table,
    TableColumn,
    TheMask,
  },
})
export default class EmpresasPendenteAprovacao extends Vue {
  private companies: Company[] = [];
  private selectedCompany: Company = new Company();
  private estados = ['AC', 'AL', 'AP', 'AM', 'BA', 'CE', 'DF', 'ES', 'GO', 'MA', 'MT', 'MS', 'MG', 'PA', 'PB', 'PR', 'PE', 'PI', 'RJ', 'RN', 'RS', 'RO', 'RR', 'SC', 'SP', 'SE', 'TO'];
  private quantidade: number = 0;

  private async mounted() {
    await this.loadData();
  }

  private async loadData() {
    this.companies = await companyService.getPendingApproval();
  }

  private openItem(item: Company, cell: TableColumn, event: any) {
    this.selectedCompany = item;
    (this.$refs['aprovar-empresa'] as any).show();
  }

  private async aprovar() {
    await companyService.approve(this.selectedCompany, this.quantidade);
    await this.loadData();
  }
}
