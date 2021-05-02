import axios from 'axios';
import Partner from '@/models/partner.model';

class PartnerService {
    public async getPartners(): Promise<Partner[]> {
        const response = await axios.get<Partner[]>('/api/Partners/All');
        return response?.data?.map((partner) => new Partner(partner)) || [];
    }
}

export default new PartnerService();
