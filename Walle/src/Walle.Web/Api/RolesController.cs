using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Walle.Core.Dtos;
using Walle.Infrastructure.Authentication;

namespace Walle.Web.Api
{
    public class RolesController : BaseApiController
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public RolesController(RoleManager<Role> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var result = _mapper.Map<List<Role>, List<RoleDto>>(roles);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            var result = _mapper.Map<Role, RoleDto>(role);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody]RoleDto input)
        {
            var existingRole = await _roleManager.FindByNameAsync(input.Name);
            if (existingRole != null)
                return BadRequest();
            var role = new Role()
            {
                Name = input.Name
            };
            await _roleManager.CreateAsync(role);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateRole([FromBody]RoleDto input)
        {
            var existingRole = await _roleManager.FindByNameAsync(input.Name);
            if (existingRole != null)
                return BadRequest();
            var role = await _roleManager.FindByIdAsync(input.Id.ToString());
            role.Name = input.Name;
            await _roleManager.UpdateAsync(role);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            await _roleManager.DeleteAsync(role);
            return Ok();
        }
    }
}
