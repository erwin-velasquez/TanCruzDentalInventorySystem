using IdentityManagement.Entities;
using IdentityManagement.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityManagement.IdentityStore
{
	public class ApplicationGroupStore :
		IGroupStore<ApplicationGroup>
	{
		public IQueryable<ApplicationGroup> Groups
		{
			get
			{
				return GroupRepository.GetGroups().AsQueryable();
			}
		}

		public async Task CreateAsync(ApplicationGroup group)
		{
			await Task.Factory.StartNew(() =>
			{
				GroupRepository.CreateNewGroup(group);
			});
		}

		public async Task DeleteAsync(ApplicationGroup group)
		{
			await Task.Factory.StartNew(() =>
			{
				GroupRepository.DeleteGroup(group);
			});
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public async Task<ApplicationGroup> FindByIdAsync(string groupId)
		{
			return await Task.Factory.StartNew(() =>
			{
				var group = GroupRepository.GetGroupById(groupId);
				return group;
			});
		}

		public async Task<ApplicationGroup> FindByNameAsync(string groupId)
		{
			return await Task.Factory.StartNew(() =>
			{
				var group = GroupRepository.GetGroupByName(groupId);
				return group;
			});
		}

		public async Task UpdateAsync(ApplicationGroup group)
		{
			await Task.Factory.StartNew(() =>
			{
				return GroupRepository.UpdateGroup(group);
			});
		}

		public async Task<IQueryable<ApplicationGroup>> GetUserGroups(string userId)
		{
			return await Task.Factory.StartNew(() =>
			{
				IQueryable<ApplicationGroup> groups = GroupRepository.GetUserGroups(userId).AsQueryable();
				return groups;
			});
		}
		public async Task RemoveUserFromGroupAsync(string userId, string groupId)
		{
			await Task.Factory.StartNew(() =>
			{
				GroupRepository.RemoveUserFromGroup(userId, groupId);
				return;
			});
		}

		public async Task RemoveRolesFromGroupAsync(string groupId, string roleId)
		{
			await Task.Factory.StartNew(() =>
			{
				GroupRepository.RemoveRoleFromGroup(groupId, roleId);
				return;
			});
		}

		public async Task AddUserToGroupAsync(string userId, string groupId)
		{
			await Task.Factory.StartNew(() =>
			{
				GroupRepository.AddUserToGroup(userId, groupId);
				return;
			});
		}

		public async Task AddRoleToGroupAsync(string groupId, string roleId)
		{
			await Task.Factory.StartNew(() =>
			{
				GroupRepository.AddRoleToGroup(groupId, roleId);
				return;
			});
		}

		public async Task<IQueryable<ApplicationRole>> GetGroupRoles(string groupId)
		{
			return await Task.Factory.StartNew(() =>
			{
				var roles = GroupRepository.GetGroupRoles(groupId).AsQueryable();
				return roles;
			});
		}
	}
}
