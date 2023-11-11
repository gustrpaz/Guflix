"use client"
import { api } from "@/lib/api";
import { useEffect, useState } from "react";
import Image from 'next/image'
import Trash from "../../assets/Trash.png"
interface Filme {
  idFilme: number,
  Genero: number,
  nomeFilme: string
}
export default function Filme() {
  const [filmes, setFilmes] = useState<Filme[]>([]);
  const [nomeFilme, setNomeFilme] = useState('');
  const [nomeGenero, setNomeGenero] = useState('');
  const [filmeSelecionado, setFilmeSelecionado] = useState(null);
  async function ListarFilmes() {
    try {
      const Resposta = await api.get('/Filmes');
      const ListaFilmes: Filme[] = Resposta.data;
      setFilmes(ListaFilmes);
    } catch (erro) {
      console.error('Erro ao realizar requisição:', erro);
    }
  }
  async function DeletarFilme(idFilme: number) {
    try {
      await api.delete(`/Filmes/${idFilme}`);
      ListarFilmes();
    } catch (erro) {
      console.error('Erro ao fazer requisição:', erro);
    }
  }
  async function CadastrarFilmes(nomeFilme: string, nomeGenero: string) {
    try {
      const FilmeData = {
        nomeFilme: nomeFilme,
        genero: nomeGenero,
      };

      await api.post('/Filmes', FilmeData);
      console.log('Filme cadastrado com sucesso!');
      ListarFilmes();
    }
    catch (erro) {
      console.error('Erro ao fazer requisição', erro);
    }
  }
  async function AtualizarFilme(idFilme: number) {
    try {
      const FilmeAtualizado = {
        nomeFilme: nomeFilme,
      };

      await api.put(`/Filmes/${idFilme}`, FilmeAtualizado);
      console.log('Filme atualizado com sucesso!')
      ListarFilmes();
    }
    catch (erro) {
      console.error('Erro ao fazer requisição', erro)
    }
  }
  useEffect(() => {
    ListarFilmes();
  }, []);
  return (
    <main>
      <table>
        <thead>
          <tr>
            <th>Cadastrar um Filme</th>
          </tr>
        </thead>

        <tbody>
          <input
            type="text"
            placeholder="Digite o nome"
            value={nomeFilme}
            onChange={(f) => setNomeFilme(f.target.value)}
          />
          <button onClick={() => CadastrarFilmes(nomeFilme, nomeGenero)}>Cadastrar</button>
        </tbody>
      </table>
      <table>
        <thead>
          <tr>
            <th>Filmes</th>
          </tr>
        </thead>
        <tbody>
          {filmes
            .map((filme) => (
              <tr key={filme.idFilme}>
                <td >{filme.nomeFilme}</td>
                <td >
                  <button className='btn' onClick={() => DeletarFilme(filme.idFilme)}>
                    <Image src={Trash} width={21} height={21} alt='Apagar' />
                  </button>
                </td>
              </tr>
            ))}
          <form onSubmit={AtualizarFilme}>
            <div>
              <label htmlFor="filmeSelect">Selecione um filme:</label>
              <select 
              id="filme" 
              value={filmeSelecionado} 
              onChange={() => setFilmeSelecionado(g.target.value)}>
                <option value="" selected>Selecione um gênero</option>
              {filmes.map((filme) => (
                  <option key={filme.idFilme} value={filme.idFilme}>
                    {filme.nomeFilme}
                  </option>
                ))}
              </select>
            </div>
            <div>
              <label htmlFor="textInput">Novo nome Filme</label>
              <input type="text" id="textInput" placeholder="Ex Avatar"></input>
            </div>
            <div>
              <button type="submit">ATUALIZAR</button>
            </div>
          </form>
        </tbody>
      </table>
    </main>
  )
}