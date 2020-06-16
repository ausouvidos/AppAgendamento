import { Component, Vue, Prop } from 'vue-property-decorator';
import Professional from '@/models/professional.model';

@Component
export default class EquipeItem extends Vue {
  @Prop({ default: [] })private items?: Professional[];
  @Prop({ default: '' })private title?: string;
}
