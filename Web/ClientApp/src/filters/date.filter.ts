import moment from 'moment';

moment.locale('pt-br');

export default (date: Date, format = 'DD/MM/YYYY') => {
  return date ? moment(date).format(format) : '';
};
