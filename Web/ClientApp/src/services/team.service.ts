import axios from 'axios';
import Professional from '@/models/professional.model';

class TeamService {
  public async getMembers(): Promise<Professional[]> {
    const url = '/api/Team/Members';
    const response = await axios.get<Professional[]>(url);
    return response.data.map((item) => new Professional(item));
  }
}

export default new TeamService();
