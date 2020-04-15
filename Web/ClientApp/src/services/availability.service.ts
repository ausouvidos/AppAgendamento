import axios from 'axios';
import moment from 'moment';
import Availability from '@/models/availability.model';
import AvailabilityDate from '@/models/availability-date.model';
import ReserveSpotRequest from '@/models/reserve-spot-request.model';
import AddAvailabilitiesRequest from '@/models/add-availabilities-request.model';

class AvailabilityService {
  public async add(data: AddAvailabilitiesRequest): Promise<boolean> {
    const url = '/api/Availabilities/Add';
    const response = await axios.post(url, data);
    return response.data;
  }

  public async getMyAvailabilities(): Promise<Availability[]> {
    const url = '/api/Availabilities/MyAvailabilities';
    const response = await axios.get<Availability[]>(url, {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem('msal.idtoken')}`,
      },
    });
    return response.data;
  }

  public async getWeeklyAvailableSpots(date = new Date()): Promise<AvailabilityDate[]> {
    // const url = `/api/Availabilities/WeeklyAvailableSpots/${moment(date).format('YYYY-MM-DD')}`;
    // const response = await axios.get<AvailabilityDate[]>(url);
    // return response.data;
    const day1 = moment().startOf('day').add(1, 'day');
    const day2 = moment().startOf('day').add(2, 'day');
    return [
      { start: day1.hour(10).toDate(), end: day1.hour(11).toDate() },
      { start: day1.hour(11).toDate(), end: day1.hour(12).toDate() },
      { start: day2.hour(16).toDate(), end: day2.hour(17).toDate() },
    ];
  }

  public async reserveSpot(data: ReserveSpotRequest): Promise<boolean> {
    const url = '/api/Availabilities/ReserveSpot';
    const response = await axios.post(url, data);
    return response.data;
  }
}

export default new AvailabilityService();
