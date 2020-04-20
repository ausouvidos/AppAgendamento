import axios from 'axios';
import moment from 'moment';
import Availability from '@/models/availability.model';
import AvailabilityDate from '@/models/availability-date.model';
import ReserveSpotRequest from '@/models/reserve-spot-request.model';
import AddAvailabilitiesRequest from '@/models/add-availabilities-request.model';

class AvailabilityService {
  public async add(data: AddAvailabilitiesRequest): Promise<boolean> {
    const url = '/api/Availabilities/Add';
    const response = await axios.post(url, data, {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem('msal.idtoken')}`,
      },
    });
    return response.data;
  }

  public async getMyAvailabilities(): Promise<Availability[]> {
    const url = '/api/Availabilities/MyAvailabilities';
    const response = await axios.get<Availability[]>(url, {
      headers: {
        Authorization: `Bearer ${sessionStorage.getItem('msal.idtoken')}`,
      },
    });
    return response.data.map((item) => new Availability(item));
  }

  public async getWeeklyAvailableSpots(date = new Date()): Promise<AvailabilityDate[]> {
    const url = `/api/Availabilities/WeeklyAvailableSpots/${moment(date).format('YYYY-MM-DD')}`;
    const response = await axios.get<AvailabilityDate[]>(url);
    return response.data.map((item) => new AvailabilityDate(item));
  }

  public async reserveSpot(data: ReserveSpotRequest): Promise<boolean> {
    const url = '/api/Availabilities/ReserveSpot';
    const response = await axios.post(url, data);
    if (response.data) {
      data.saveCache();
    }
    return response.data;
  }
}

export default new AvailabilityService();
