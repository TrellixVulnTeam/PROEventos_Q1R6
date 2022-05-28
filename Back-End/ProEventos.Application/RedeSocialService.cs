using AutoMapper;
using ProEventos.Application.Contratos;
using ProEventos.Application.Dtos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEventos.Application
{
    public class RedeSocialService : IRedeSocialService
    {
        private readonly IRedeSocialPersistence _redeSocialPersistence;
        private readonly IMapper _mapper;

        public RedeSocialService(IRedeSocialPersistence redeSocialPersistence,
                           IMapper mapper)
        {
            _redeSocialPersistence = redeSocialPersistence;
            _mapper = mapper;
        }

        public async Task AddRedeSocial(int Id, RedeSocialDto model, bool isEvento)
        {
            try
            {
                var redeSocial = _mapper.Map<RedeSocial>(model);
                if (isEvento)
                {
                    redeSocial.EventoID = Id;
                    redeSocial.PalestranteID = null;
                }
                else
                {
                    redeSocial.EventoID = null;
                    redeSocial.PalestranteID = Id;
                }

                _redeSocialPersistence.Add<RedeSocial>(redeSocial);

                await _redeSocialPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RedeSocialDto[]> SaveByEvento(int eventoId, RedeSocialDto[] models)
        {
            try
            {
                var RedeSocials = await _redeSocialPersistence.GetAllByEventoIdAsync(eventoId);
                if (RedeSocials == null) return null;

                foreach (var model in models)
                {
                    if (model.ID == 0)
                    {
                        await AddRedeSocial(eventoId, model, true);
                    }
                    else
                    {
                        var RedeSocial = RedeSocials.FirstOrDefault(RedeSocial => RedeSocial.ID == model.ID);
                        model.EventoID = eventoId;

                        _mapper.Map(model, RedeSocial);

                        _redeSocialPersistence.Update<RedeSocial>(RedeSocial);

                        await _redeSocialPersistence.SaveChangesAsync();
                    }
                }

                var RedeSocialRetorno = await _redeSocialPersistence.GetAllByEventoIdAsync(eventoId);

                return _mapper.Map<RedeSocialDto[]>(RedeSocialRetorno);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RedeSocialDto[]> SaveByPalestrante(int palestranteId, RedeSocialDto[] models)
        {
            try
            {
                var redeSocials = await _redeSocialPersistence.GetAllByPalestranteIdAsync(palestranteId);
                if (redeSocials == null) return null;

                foreach (var model in models)
                {
                    if (model.ID == 0)
                    {
                        await AddRedeSocial(palestranteId, model, false);
                    }
                    else
                    {
                        var redeSocial = redeSocials.FirstOrDefault(redeSocial => redeSocial.ID == model.ID);
                        model.PalestranteID = palestranteId;

                        _mapper.Map(model, redeSocial);

                        _redeSocialPersistence.Update<RedeSocial>(redeSocial);

                        await _redeSocialPersistence.SaveChangesAsync();
                    }
                }

                var redeSocialRetorno = await _redeSocialPersistence.GetAllByPalestranteIdAsync(palestranteId);

                return _mapper.Map<RedeSocialDto[]>(redeSocialRetorno);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteByEvento(int eventoId, int redeSocialId)
        {
            try
            {
                var redeSocial = await _redeSocialPersistence.GetRedeSocialEventoByIdsAsync(eventoId, redeSocialId);
                if (redeSocial == null) throw new Exception("Rede Social por Evento para delete não encontrado.");

                _redeSocialPersistence.Delete<RedeSocial>(redeSocial);
                return await _redeSocialPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteByPalestrante(int palestranteId, int redeSocialId)
        {
            try
            {
                var redeSocial = await _redeSocialPersistence.GetRedeSocialPalestranteByIdsAsync(palestranteId, redeSocialId);
                if (redeSocial == null) throw new Exception("Rede Social por Palestrante para delete não encontrado.");

                _redeSocialPersistence.Delete<RedeSocial>(redeSocial);
                return await _redeSocialPersistence.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RedeSocialDto[]> GetAllByEventoIdAsync(int eventoId)
        {
            try
            {
                var redeSocials = await _redeSocialPersistence.GetAllByEventoIdAsync(eventoId);
                if (redeSocials == null) return null;

                var resultado = _mapper.Map<RedeSocialDto[]>(redeSocials);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RedeSocialDto[]> GetAllByPalestranteIdAsync(int palestranteId)
        {
            try
            {
                var redeSocials = await _redeSocialPersistence.GetAllByPalestranteIdAsync(palestranteId);
                if (redeSocials == null) return null;

                var resultado = _mapper.Map<RedeSocialDto[]>(redeSocials);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RedeSocialDto> GetRedeSocialEventoByIdsAsync(int eventoId, int redeSocialId)
        {
            try
            {
                var redeSocial = await _redeSocialPersistence.GetRedeSocialEventoByIdsAsync(eventoId, redeSocialId);
                if (redeSocial == null) return null;

                var resultado = _mapper.Map<RedeSocialDto>(redeSocial);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RedeSocialDto> GetRedeSocialPalestranteByIdsAsync(int palestranteId, int redeSocialId)
        {
            try
            {
                var redeSocial = await _redeSocialPersistence.GetRedeSocialPalestranteByIdsAsync(palestranteId, redeSocialId);
                if (redeSocial == null) return null;

                var resultado = _mapper.Map<RedeSocialDto>(redeSocial);

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
