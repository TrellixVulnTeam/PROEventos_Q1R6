﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.Persistence;
using ProEventos.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProEventos.Persistence.Contexto;
using ProEventos.Application.Contratos;
using Microsoft.AspNetCore.Http;
using ProEventosAPI.Application.Dtos;
using ProEventos.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using ProEventosAPI.Extensions;

namespace ProEventosAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RedesSociaisController : ControllerBase
    {
        private readonly IRedeSocialService _redeSocialService;
        private readonly IEventoService _eventoService;
        private readonly IPalestranteService _palestranteService;

        public RedesSociaisController(IRedeSocialService RedeSocialService,
                                      IEventoService eventoService,
                                      IPalestranteService palestranteService)
        {
            _palestranteService = palestranteService;
            _redeSocialService = RedeSocialService;
            _eventoService = eventoService;
        }

        [HttpGet("evento/{eventoId}")]
        public async Task<IActionResult> GetByEvento(int eventoId)
        {
            try
            {
                if (!(await AutorEvento(eventoId)))
                    return Unauthorized();

                var redeSocial = await _redeSocialService.GetAllByEventoIdAsync(eventoId);
                if (redeSocial == null) return NoContent();

                return Ok(redeSocial);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar Rede Social por Evento. Erro: {ex.Message}");
            }
        }

        [HttpGet("palestrante")]
        public async Task<IActionResult> GetByPalestrante()
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestranteByUserIdAsync(User.GetUserId());
                if (palestrante == null) return Unauthorized();

                var redeSocial = await _redeSocialService.GetAllByPalestranteIdAsync(palestrante.ID);
                if (redeSocial == null) return NoContent();

                return Ok(redeSocial);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar Rede Social por Palestrante. Erro: {ex.Message}");
            }
        }


        [HttpPut("evento/{eventoId}")]
        public async Task<IActionResult> SaveByEvento(int eventoId, RedeSocialDto[] models)
        {
            try
            {
                if (!(await AutorEvento(eventoId)))
                    return Unauthorized();

                var redeSocial = await _redeSocialService.SaveByEvento(eventoId, models);
                if (redeSocial == null) return NoContent();

                return Ok(redeSocial);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar salvar Rede Social por Evento. Erro: {ex.Message}");
            }
        }

        [HttpPut("palestrante")]
        public async Task<IActionResult> SaveByPalestrante(RedeSocialDto[] models)
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestranteByUserIdAsync(User.GetUserId());
                if (palestrante == null) return Unauthorized();

                var redeSocial = await _redeSocialService.SaveByPalestrante(palestrante.ID, models);
                if (redeSocial == null) return NoContent();

                return Ok(redeSocial);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar salvar Rede Social por Palestrante. Erro: {ex.Message}");
            }
        }

        [HttpDelete("evento/{eventoId}/{redeSocialId}")]
        public async Task<IActionResult> DeleteByEvento(int eventoId, int redeSocialId)
        {
            try
            {
                if (!(await AutorEvento(eventoId)))
                    return Unauthorized();

                var RedeSocial = await _redeSocialService.GetRedeSocialEventoByIdsAsync(eventoId, redeSocialId);
                if (RedeSocial == null) return NoContent();

                return await _redeSocialService.DeleteByEvento(eventoId, redeSocialId)
                       ? Ok(new { message = "Rede Social Deletada" })
                       : throw new Exception("Ocorreu um problem não específico ao tentar deletar Rede Social por Evento.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar Rede Social por Evento. Erro: {ex.Message}");
            }
        }

        [HttpDelete("palestrante/{redeSocialId}")]
        public async Task<IActionResult> DeleteByPalestrante(int redeSocialId)
        {
            try
            {
                var palestrante = await _palestranteService.GetPalestranteByUserIdAsync(User.GetUserId());
                if (palestrante == null) return Unauthorized();

                var RedeSocial = await _redeSocialService.GetRedeSocialPalestranteByIdsAsync(palestrante.ID, redeSocialId);
                if (RedeSocial == null) return NoContent();

                return await _redeSocialService.DeleteByPalestrante(palestrante.ID, redeSocialId)
                       ? Ok(new { message = "Rede Social Deletada" })
                       : throw new Exception("Ocorreu um problem não específico ao tentar deletar Rede Social por Palestrante.");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar Rede Social por Palestrante. Erro: {ex.Message}");
            }
        }

        [NonAction]
        private async Task<bool> AutorEvento(int eventoId)
        {
            var evento = await _eventoService.GetAllEventoByIdAsync(User.GetUserId(), eventoId, false);
            if (evento == null) return false;

            return true;
        }
    }
}