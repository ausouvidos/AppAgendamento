import { localize, configure, extend } from 'vee-validate';
import { required, email } from 'vee-validate/dist/rules';
import pt_BR from 'vee-validate/dist/locale/pt_BR.json';

localize('pt_BR', pt_BR);

configure({ classes: { invalid: 'is-invalid' } });

extend('required', required);
extend('email', email);
extend('phone', (value) => /^[\d]{10,11}$/.test(value) ? true : 'O campo {_field_} deve ser um telefone válido');
