import moment from 'moment';

moment.locale('pt-br');

export default (date: Date, format = 'DD/MM/YYYY') => {
  return moment(date).format(format);
};
