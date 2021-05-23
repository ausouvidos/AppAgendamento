import moment from 'moment';
import AvailabilityDate from './availability-date.model';
import availabilityService from '@/services/availability.service';

interface SpotScheduleDay {
  date: Date;
  availableSpots: AvailabilityDate[];
}

export default class SpotSchedule {
  public weekStart: Date;
  public days: SpotScheduleDay[];

  get weekEnd() {
    return moment(this.weekStart)
      .endOf('week')
      .toDate();
  }

  get displayTitle() {
    const weekStartFormat = moment(this.weekStart).isSame(this.weekEnd, 'month')
      ? 'D'
      : 'D [de] MMMM';
    return `Semana de ${moment(this.weekStart)
      .format(weekStartFormat)
      .toLowerCase()} a ${moment(this.weekEnd)
      .format('LL')
      .toLowerCase()}`;
  }

  get isFirstWeek() {
    return moment().isSameOrAfter(this.weekStart);
  }

  constructor() {
    this.weekStart = moment()
      .startOf('week')
      .toDate();
    this.days = [];
  }

  public async load(code: string = '') {
    this.days = [];
    const weekAvailableSpots = await availabilityService.getWeeklyAvailableSpots(
      this.weekStart,
      code,
    );

    weekAvailableSpots.forEach((spot) => {
      const lastDay =
        this.days.length > 0 ? this.days[this.days.length - 1] : null;
      const spotDate = moment(spot.start).startOf('day');

      if (lastDay && spotDate.isSame(lastDay.date)) {
        lastDay.availableSpots.push(spot);
      } else {
        this.days.push({
          date: spotDate.toDate(),
          availableSpots: [spot],
        });
      }
    });
  }

  public async previousWeek() {
    this.weekStart = moment(this.weekStart)
      .subtract(1, 'week')
      .toDate();
    await this.load();
  }

  public async nextWeek() {
    this.weekStart = moment(this.weekStart)
      .add(1, 'week')
      .toDate();
    await this.load();
  }
}
