"use client"
import Image from 'next/image'
import Logo from '../assets/Logo.svg'
import { useEffect, useState } from "react";
import { useRouter } from 'next/navigation'
import './header.css'
import { api } from '@/lib/api';

export function Header() {
  const router = useRouter();
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [User, setUser] = useState('')

  async function ListarUserName() {
    try {
      const token = localStorage.getItem('token');

      if (!token) {
        console.error('Token não encontrado.');
        return;
      }
      const [, payloadBase64] = token.split('.');
      const decodedPayload = JSON.parse(atob(payloadBase64));
      const jti = decodedPayload.jti;
      if (jti) {
        const UserName = await api.get(`/Usuarios/${jti}`);
        setUser(UserName.data.userName)
      } else {
        console.error('JTI não encontrado no token.');
      }
    } catch (erro) {
      console.error('Erro ao realizar requisição:', erro);
    }
  }

  useEffect(() => {
    const checkAuth = () => {
      const token = localStorage.getItem('token');
      return !!token;
    };

    ListarUserName()

    setIsLoggedIn(checkAuth());
  }, [isLoggedIn]);

  const handleLogout = () => {
    localStorage.removeItem('token')
    setIsLoggedIn(false)
    router.replace('/login')
    window.location.reload();
  }

  return (
    <header>
      <div className='wrapper'>
        <div className={`content-header ${isLoggedIn ? 'user-logged-in' : 'user-not-logged-in'}`}>
          {isLoggedIn && <span>Olá, {User}</span>}
          <Image src={Logo} width={160} height={53} alt='Logo'></Image>
          {isLoggedIn && (<button className='logout' onClick={handleLogout}>Logout</button>)}
        </div>
      </div>
    </header>
  );
}
