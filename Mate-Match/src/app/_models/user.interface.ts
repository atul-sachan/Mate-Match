import { Photo } from './photo.interface';

export interface User {
  id: number;
  username: string;
  gender: string;
  age: number;
  knownAs: string;
  created: string;
  lastActive: string;
  city: string;
  country: string;
  photoUrl: string;
  interests?: string;
  introduction?: string;
  lookingFor?: string;
  photos?: Photo[];
}
