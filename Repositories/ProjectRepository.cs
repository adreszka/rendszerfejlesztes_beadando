using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using rendszerfejlesztes_beadando.BusinessLogic;
using rendszerfejlesztes_beadando.Data;
using rendszerfejlesztes_beadando.Models;
using rendszerfejlesztes_beadando.Models.Entities;

namespace rendszerfejlesztes_beadando.Repositories
{
    public class ProjectRepository
    {
        private readonly DataContext _context;
        private readonly IMapper mapper;
        public ProjectRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public async Task<bool> addNewProject(NewProject parameters)
        {
            var customer = await _context.Customers.FirstOrDefaultAsync(c =>
            c.Name == parameters.Name && c.PhoneNumber == parameters.PhoneNumber);
            if (customer == null)
            {
                var c = new Customer();
                if (parameters.TaxNumber == "")
                {
                    c.Name = parameters.Name;
                    c.PhoneNumber = parameters.PhoneNumber;
                    c.Email = parameters.Email;
                }
                else
                {
                    c.Name = parameters.Name;
                    c.PhoneNumber = parameters.PhoneNumber;
                    c.Email = parameters.Email;
                    c.TaxNumber = parameters.TaxNumber;
                }
                await _context.AddAsync(c);
            }
            await _context.SaveChangesAsync();

            if (customer == null)
            {
                customer = await _context.Customers.FirstAsync(c =>
                c.Name == parameters.Name && c.PhoneNumber == parameters.PhoneNumber);
            }
            var project = new Project
            {
                Location = parameters.Location,
                Description = parameters.Description,
                CustomerId = customer.Id,
            };
            await _context.AddAsync(project);
            await _context.SaveChangesAsync();

            var p = await _context.Projects.FirstAsync(p => p.Location == parameters.Location);
            var statuses = await _context.Statuses.FirstAsync(s => s.Name == "New");
            var log = new Log
            {
                ProjectId = project.Id,
                StatusId = statuses.Id,
            };
            await _context.AddAsync(log);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddWorkTimeAndFee(WorkTimeAndFee timeandfee)
        {
            var projects = await _context.Projects.FirstAsync(p => p.Location == timeandfee.Location);
            projects.WorkTime = timeandfee.Worktime;
            projects.Fee = timeandfee.Fee;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> AddComponentToProject(AddComponentToProject component)
        {
            var project = await _context.Projects.FirstAsync(p => p.Location == component.Location);
            var components = await _context.Components.FirstAsync(c => c.Name == component.Name);
            var projectComponents = await _context.ProjectsComponents.ToListAsync();
            var storage = await _context.Storage.ToListAsync();
            int componentQuantityToStore = component.Quantity;
            foreach (var s in storage) 
            {
                int reservedQuantityOfThatComponentInSameStorage = 0;
                if (s.ComponentId == components.Id) 
                {
                    foreach (var pc in projectComponents) 
                    {
                        if (pc.ComponentId == components.Id && pc.StorageId == s.Id)
                        {
                            reservedQuantityOfThatComponentInSameStorage += pc.Quantity;
                        }
                    }
                    if (reservedQuantityOfThatComponentInSameStorage < s.Quantity) 
                    {
                        int storableQuantity = (int)s.Quantity - reservedQuantityOfThatComponentInSameStorage;
                        ProjectComponent? foundProjectComponent = null;
                        foreach (var pc in projectComponents)
                        {
                            if (pc.ProjectId == project.Id && pc.StorageId == s.Id)
                            {
                                foundProjectComponent = pc;
                                break;
                            }
                        }
                        if (componentQuantityToStore <= storableQuantity)
                        {
                            if (foundProjectComponent == null)
                            {
                                var projectComponent = new ProjectComponentModel
                                {
                                    ProjectId = project.Id,
                                    ComponentId = components.Id,
                                    StorageId = s.Id,
                                    Quantity = componentQuantityToStore,
                                };
                                componentQuantityToStore -= componentQuantityToStore;
                                await _context.AddAsync(mapper.Map<ProjectComponent>(projectComponent));
                                await _context.SaveChangesAsync();
                            }
                            else 
                            {
                                foundProjectComponent.Quantity += componentQuantityToStore;
                                componentQuantityToStore -= componentQuantityToStore;
                            }
                        }
                        else 
                        {
                            if (foundProjectComponent == null)
                            {
                                var projectComponent = new ProjectComponentModel
                                {
                                    ProjectId = project.Id,
                                    ComponentId = components.Id,
                                    StorageId = s.Id,
                                    Quantity = storableQuantity,
                                };
                                componentQuantityToStore -= storableQuantity;
                                await _context.AddAsync(mapper.Map<ProjectComponent>(projectComponent));
                                await _context.SaveChangesAsync();
                            }
                            else 
                            {
                                if (componentQuantityToStore <= storableQuantity)
                                {
                                    foundProjectComponent.Quantity += componentQuantityToStore - reservedQuantityOfThatComponentInSameStorage;
                                    componentQuantityToStore -= componentQuantityToStore - reservedQuantityOfThatComponentInSameStorage;
                                }
                                else 
                                {
                                    foundProjectComponent.Quantity += storableQuantity;
                                    componentQuantityToStore -= storableQuantity;
                                }
                            }
                        }
                        }
                    }
                    if (componentQuantityToStore == 0) break;
                }
                if (componentQuantityToStore != 0)
                {
                ProjectComponent? foundProjectComponent = null;
                foreach (var pc in projectComponents)
                {
                    if (pc.ProjectId == project.Id && pc.ComponentId == components.Id && pc.StorageId == null)
                    {
                        foundProjectComponent = pc;
                        break;
                    }
                }
                if (foundProjectComponent == null)
                {
                    var projectComponent = new ProjectComponentModel
                    {
                        ProjectId = project.Id,
                        ComponentId = components.Id,
                        StorageId = null,
                        Quantity = componentQuantityToStore,
                    };
                    await _context.AddAsync(mapper.Map<ProjectComponent>(projectComponent));
                }
                else
                {
                    foundProjectComponent.Quantity += componentQuantityToStore;
                }
                }
                var statuses = await _context.Statuses.FirstAsync(s => s.Name == "Draft");
                var log = await _context.Logs.FirstOrDefaultAsync(g => g.ProjectId == project.Id && g.StatusId == statuses.Id);
                if (log == null)
                {
                    var logs = new Log
                    {
                        ProjectId = project.Id,
                        StatusId = statuses.Id,
                    };
                    await _context.AddAsync(logs);
                }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _context.Projects.ToListAsync();
        }

        public async Task<IEnumerable<StoreComponent>> GetProjectComponents(string location)
        {
            var project = await _context.Projects.FirstAsync(p => p.Location == location);
            var projectComponents = await _context.ProjectsComponents.Where(pc => pc.ProjectId == project.Id).
                OrderBy(pc => pc.ComponentId).ToListAsync();
            List<StoreComponent> storeComponents = new List<StoreComponent>();
            var comp = await _context.Components.ToListAsync();
            Dictionary<string, int> componentsQuantity = new Dictionary<string, int>();
            foreach (var c in comp)
            {
                componentsQuantity.Add(c.Name, 0);
            }
            foreach (var projectComponent in projectComponents) 
            {
                var component = await _context.Components.FirstAsync(c => c.Id == projectComponent.ComponentId);
                if (projectComponent.ComponentId == component.Id) 
                {
                    componentsQuantity[component.Name] += projectComponent.Quantity;
                }
            }
            foreach (var cq in componentsQuantity) 
            {
                if (cq.Value > 0)
                {
                    var components = new StoreComponent
                    {
                        Name = cq.Key,
                        Quantity = cq.Value,
                    };
                    storeComponents.Add(components);
                }
            }
            return storeComponents;
        }

        public async Task<ProjectStatus> GetProjectsWithStatus(string location)
        {
            var p = await _context.Projects.FirstAsync(p => p.Location == location);
            var log = await _context.Logs.OrderByDescending(l => l.Date).FirstAsync(l => l.ProjectId == p.Id);
            var status = await _context.Statuses.FirstAsync(s => s.Id == log.StatusId);
            var project = new ProjectStatus
            {
                StatusName = status.Name,
            };
            return project;
        }

        public async Task<bool> PriceCalculation(string location) 
        {
            bool componentsAvailable = true;
            var project = await _context.Projects.FirstAsync(p => p.Location == location);
            var projectComponents = await _context.ProjectsComponents.Where(pc => 
                pc.ProjectId == project.Id).ToListAsync();
            foreach (var projectComponent in projectComponents) 
            {
                if (projectComponent.StorageId == null) 
                {
                    var status = await _context.Statuses.FirstAsync(s => s.Name == "Wait");
                    var projectStatus = await _context.Logs.OrderByDescending(ps => ps.Date).ToListAsync();
                    foreach (var ps in projectStatus) 
                    {
                        if (ps.StatusId == status.Id) 
                        {
                            componentsAvailable = false;
                            break;
                        }
                    }
                    if (componentsAvailable) 
                    {
                        var newLog = new Log
                        {
                            ProjectId = project.Id,
                            StatusId = status.Id,
                        };
                        await _context.AddAsync(newLog);
                        await _context.SaveChangesAsync();
                        componentsAvailable = false;
                    }
                }
            }
            if (!componentsAvailable) 
            {
                return false;
            }
            var stat = await _context.Statuses.FirstAsync(s => s.Name == "Scheduled");
            var logs = await _context.Logs.OrderByDescending(l => l.Date).ToListAsync();
            foreach (var l in logs) 
            {
                if (l.ProjectId == project.Id && l.StatusId == stat.Id) 
                {
                    return false;
                }
            }
            var components = await _context.Components.ToListAsync();
            int sum = 0;
            Dictionary<string, int> componentsQuantity = new Dictionary<string, int>();
            foreach (var component in components) 
            {
                componentsQuantity[component.Name] = 0;
            }
            foreach (var pc in projectComponents) 
            {
                if (pc.StorageId != null)
                {
                    var component = await _context.Components.FirstAsync(c => c.Id == pc.ComponentId);
                    componentsQuantity[component.Name] += pc.Quantity;
                }
            }
            foreach (var cq in componentsQuantity) 
            {
                if (cq.Value > 0) 
                {
                    var component = components.First(c => c.Name == cq.Key);
                    sum += cq.Value * component.Price;
                }
            }
            var projectPrice = await _context.Projects.FirstAsync(pp => pp.Id == project.Id);
            projectPrice.ComponentsPrices = sum;
            var log = new Log
            {
                ProjectId = project.Id,
                StatusId = stat.Id,
            };
            await _context.AddAsync(log);
            await _context.SaveChangesAsync();
            return componentsAvailable;
        }

        public async Task<IEnumerable<MissingComponents>> GetProjectsComponentsInformation()
        {
            List<MissingComponents> missingComponents = new List<MissingComponents>();
            List<StoreComponent> reservedComponents = new List<StoreComponent>();
            List<StoreComponent> missComponents = new List<StoreComponent>();

            Dictionary<string, int> componentsQuantity = new Dictionary<string, int>();

            var projectComponents = await _context.ProjectsComponents.ToListAsync();
            var projects = await _context.Projects.ToListAsync();
            var components = await _context.Components.ToListAsync();

            foreach (var project in projects)
            {
                missComponents.Clear();
                reservedComponents.Clear();
                foreach (var component in components)
                {
                    componentsQuantity[component.Name] = 0;
                }
                foreach (var projectComponent in projectComponents)
                {
                    var component = components.First(c => c.Id == projectComponent.ComponentId);
                    if (projectComponent.ProjectId == project.Id && projectComponent.StorageId != null)
                    {
                        componentsQuantity[component.Name] += projectComponent.Quantity;
                    }
                }
                foreach (var c in componentsQuantity)
                {
                    if (c.Value != 0)
                    {
                        var reservedComponent = new StoreComponent
                        {
                            Name = c.Key,
                            Quantity = c.Value,
                        };
                        reservedComponents.Add(reservedComponent);
                    }
                }
                foreach (var component in components)
                {
                    componentsQuantity[component.Name] = 0;
                }
                foreach (var projectComponent in projectComponents)
                {
                    var component = components.First(c => c.Id == projectComponent.ComponentId);
                    if (projectComponent.ProjectId == project.Id && projectComponent.StorageId == null)
                    {
                        componentsQuantity[component.Name] += projectComponent.Quantity;
                    }
                }
                foreach (var c in componentsQuantity)
                {
                    if (c.Value != 0)
                    {
                        var missComponent = new StoreComponent
                        {
                            Name = c.Key,
                            Quantity = c.Value,
                        };
                        missComponents.Add(missComponent);
                    }
                }
                var mc = new MissingComponents
                {
                    Location = project.Location,
                    MissingCompsFromProjects = missComponents.ToList(),
                    ReservedComponents = reservedComponents.ToList(),
                };
                missingComponents.Add(mc);
            }

            return missingComponents;
        }

        public async Task<bool> CloseProject(ProjectClose projectClose) 
        {
            var statusC = await _context.Statuses.FirstAsync(s => s.Name == "Completed");
            var statusF = await _context.Statuses.FirstAsync(s => s.Name == "Failed");
            var project = await _context.Projects.FirstAsync(p => p.Location == projectClose.Location);
            var logs = await _context.Logs.OrderByDescending(l => l.Date).ToListAsync();
            if (projectClose.ProjectFinished == true) 
            {
                foreach (var l in logs) 
                {
                    if (l.ProjectId == project.Id && l.StatusId != 5) 
                    {
                        return false;
                    }
                }
            }
            foreach (var l in logs)
            {
                if ((l.ProjectId == project.Id && l.StatusId == statusC.Id) || (l.ProjectId == project.Id && l.StatusId == statusF.Id)) 
                {
                    return false;
                }
            }
            if (projectClose.ProjectFinished == true)
            {
                var log = new Log
                {
                    ProjectId = project.Id,
                    StatusId = statusC.Id,
                };
                await _context.AddAsync(log);
            }
            else 
            {
                var log = new Log
                {
                    ProjectId = project.Id,
                    StatusId = statusF.Id,
                };
                await _context.AddAsync(log);
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AllInformationAboutTheProject> ListProject(string location) 
        {
            //List<StoreComponent> reservedComponents = new List<StoreComponent>();
            //List<StoreComponent> missComponents = new List<StoreComponent>();
            List<PathData> compsOnProject = new List<PathData>();

            //Dictionary<string, int> componentsQuantity = new Dictionary<string, int>();

            var project = await _context.Projects.FirstAsync(p => p.Location == location);
            var projectComponents = await _context.ProjectsComponents.Where(pc => pc.ProjectId == project.Id).ToListAsync();
            var components = await _context.Components.ToListAsync();

            //foreach (var component in components)
            //{
            //    componentsQuantity[component.Name] = 0;
            //}
            //foreach (var projectComponent in projectComponents)
            //{
            //    var component = components.First(c => c.Id == projectComponent.ComponentId);
            //    if (projectComponent.StorageId != null)
            //    {
            //        componentsQuantity[component.Name] += projectComponent.Quantity;
            //    }
            //}
            //foreach (var c in componentsQuantity)
            //{
            //    if (c.Value != 0)
            //    {
            //        var reservedComponent = new StoreComponent
            //        {
            //            Name = c.Key,
            //            Quantity = c.Value,
            //        };
            //        reservedComponents.Add(reservedComponent);
            //    }
            //}
            //foreach (var component in components)
            //{
            //    componentsQuantity[component.Name] = 0;
            //}
            //foreach (var projectComponent in projectComponents)
            //{
            //    var component = components.First(c => c.Id == projectComponent.ComponentId);
            //    if (projectComponent.StorageId == null)
            //    {
            //        componentsQuantity[component.Name] += projectComponent.Quantity;
            //    }
            //}
            //foreach (var c in componentsQuantity)
            //{
            //    if (c.Value != 0)
            //    {
            //        var missComponent = new StoreComponent
            //        {
            //            Name = c.Key,
            //            Quantity = c.Value,
            //        };
            //        missComponents.Add(missComponent);
            //    }
            //}
            //var projectModel = new ProjectModel
            //{
            //    Location = project.Location,
            //    ComponentPrices = project.ComponentsPrices,
            //    Description = project.Description,
            //    Fee = project.Fee,
            //    WorkTime = project.WorkTime,
            //};
            //var customer = await _context.Customers.FirstAsync(c => c.Id == project.CustomerId);
            //var customerModel = new CustomerModel
            //{
            //    Name = customer.Name,
            //    Email = customer.Email,
            //    PhoneNumber = customer.PhoneNumber,
            //    TaxNumber = customer.TaxNumber,

            //};
            var logs = await _context.Logs.OrderByDescending(l => l.Date).Where(l => l.ProjectId == project.Id).ToListAsync();
            foreach (var l in logs)
            {
                if (l.StatusId == 7 || l.StatusId == 6 || l.StatusId == 5)
                {
                    break;
                }
                if (l.StatusId == 4)
                {
                    var la = new Log
                    {
                        ProjectId = project.Id,
                        StatusId = 5,
                    };
                    await _context.AddAsync(la);
                    await _context.SaveChangesAsync();
                }
            }
            var storage = await _context.Storage.ToListAsync();
            foreach (var s in storage) 
            {
                foreach (var projectComponent in projectComponents)
                {
                    if (s.Id == projectComponent.StorageId) 
                    {
                        var component = components.First(c => c.Id == projectComponent.ComponentId);
                        var storageLocation = new StorageLocation
                        {
                            Row = s.Row,
                            Columnn = s.Columnn,
                            Level = s.Level,
                        };
                        var componentsOnProjects = new PathData
                        {
                            ComponentName = component.Name,
                            Quantity = projectComponent.Quantity,
                            Location = storageLocation,
                        };
                        compsOnProject.Add(componentsOnProjects);
                    }
                }
            }
            if (compsOnProject.Count() != 0)
            {
                compsOnProject = Helper.FindShortestPath(compsOnProject);
            }
            var log = await _context.Logs.OrderByDescending(l => l.Date).FirstAsync(l => l.ProjectId == project.Id);
            var status = await _context.Statuses.FirstAsync(s => s.Id == log.StatusId);
            var projectInformation = new AllInformationAboutTheProject
            {
                //Project = projectModel,
                //MissingCompsFromProjects = missComponents.ToList(),
                //ReservedComponents = reservedComponents.ToList(),
                StatusName = status.Name,
                //Customer = customerModel,
                ProjectComponents = compsOnProject,
            };

            return projectInformation;
        }

        public async Task<IEnumerable<StoreComponent>> GetMissingProjectComponents(string location) 
        {
            var project = await _context.Projects.FirstAsync(p => p.Location == location);
            var projectComponents = await _context.ProjectsComponents.Where(pc => pc.ProjectId == project.Id).
                OrderBy(pc => pc.ComponentId).ToListAsync();
            List<StoreComponent> storeComponents = new List<StoreComponent>();
            var comp = await _context.Components.ToListAsync();
            Dictionary<string, int> componentsQuantity = new Dictionary<string, int>();
            foreach (var c in comp)
            {
                componentsQuantity.Add(c.Name, 0);
            }
            foreach (var projectComponent in projectComponents)
            {
                var component = await _context.Components.FirstAsync(c => c.Id == projectComponent.ComponentId);
                if (projectComponent.ComponentId == component.Id && projectComponent.StorageId == null)
                {
                    componentsQuantity[component.Name] += projectComponent.Quantity;
                }
            }
            foreach (var cq in componentsQuantity)
            {
                if (cq.Value > 0)
                {
                    var components = new StoreComponent
                    {
                        Name = cq.Key,
                        Quantity = cq.Value,
                    };
                    storeComponents.Add(components);
                }
            }
            return storeComponents;
        }

        public async Task<bool> AddComponentToProjectManual([FromBody] AddComponentToProjectManually component)
        {
            var project = await _context.Projects.FirstAsync(p => p.Location == component.AddComponentToProject.Location);
            var comp = await _context.Components.FirstAsync(c => c.Name == component.AddComponentToProject.Name);
            var storage = await _context.Storage.FirstAsync(s =>
            s.Row == component.StorageLocation.Row && s.Columnn == component.StorageLocation.Columnn
            && s.Level == component.StorageLocation.Level);
            var projectComponent = await _context.ProjectsComponents.FirstAsync(pc =>
            pc.ComponentId == comp.Id && pc.ProjectId == project.Id && pc.StorageId == null);
            if (projectComponent.Quantity < component.AddComponentToProject.Quantity) 
            {
                return false;
            }
            projectComponent.Quantity -= component.AddComponentToProject.Quantity;
            var pc = new ProjectComponent
            {
                ComponentId = comp.Id,
                ProjectId = project.Id,
                Quantity = component.AddComponentToProject.Quantity,
                StorageId = storage.Id,
            };
            await _context.AddAsync(pc);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
