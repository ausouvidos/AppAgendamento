import { Component, Vue } from 'vue-property-decorator';
import CalendarPsicologo from '@/components/calendar-psicologo/calendar-psicologo.vue';
import { TheMask } from 'vue-the-mask';
import { Table, TableColumn, Cascader } from 'element-ui';
import companyService from '@/services/company.service';
import Company from '@/models/company.model';
import teamService from '@/services/team.service';
import Professional from '@/models/professional.model';
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
  private selectedProfessionals: number[] = [];

  private async mounted() {
    await this.loadData();
  }

  private async loadData() {
      this.companies = await companyService.getPendingApproval();

      const professionals: Professional[] = (await teamService.getMembers())?.filter((a: Professional) => a.grupo === 'Equipe');
      const professionalsGrouped: IProfessionalsParser = professionals.reduce((p, c: Professional) => {
              const key: string = c?.funcao || '';
              if (key?.length > 0 && p.hasOwnProperty(key)) {
                  (p as any)[key].push(c);
              } else if (key?.length > 0) {
                  (p as any)[key] = [c];
              }
              return p;
          }, {} as IProfessionalsParser);

      this.professionals = Object.keys(professionalsGrouped).map((key: string) => ({
          value: key,
          label: key,
          children: (professionalsGrouped as any)[key]
              .map((p: Professional) => ({ value: p.id, label: p.nomeCompleto } as ICascaderChildren))
              .sort((a: ICascaderChildren, b: ICascaderChildren) => a.label.localeCompare(b.label)),
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
    this.selectedProfessionals = values.map((item: any[]) => item[1]);
  }
}
