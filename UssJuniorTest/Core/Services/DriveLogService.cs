using UssJuniorTest.Infrastructure.Repositories;
using UssJuniorTest.Core.Models;
using UssJuniorTest.Core.Utilities;

namespace UssJuniorTest.Core.Services
{
    public class DriveLogService
    {
        private DriveLogRepository _logRepository;

        private PersonRepository _personRepository;

        private CarRepository _carRepository;

        public DriveLogService(DriveLogRepository logRepo,
                                PersonRepository personRepo,
                                CarRepository carRepo)
        {
            _logRepository = logRepo;
            _personRepository = personRepo;
            _carRepository = carRepo;
        }   

        public List<DriveLogQueryData> GetDriveLogs(QueryParameters parameters) 
        {
            if (parameters == null || parameters.DateTimeRange == null || !parameters.DateTimeRange.IsFilled())
            {
                throw new ArgumentNullException("Time must always be specified and filled correctly");
            }

            var timeRangedLogs = _logRepository.GetAll()
                .Where(x => x.StartDateTime.CompareTo(parameters.DateTimeRange.Start)>=0 &&
                            x.EndDateTime.CompareTo(parameters.DateTimeRange.End)<=0);

            if (parameters.OptionalParameters == null)
            {
                
                return ProcessWithoutOptionals(timeRangedLogs).ToList();
            }

            return ProcessWithOptionals(timeRangedLogs, parameters.OptionalParameters).ToList();
        }

        private IEnumerable<DriveLogQueryData> CreateLogData(IEnumerable<Car> cars, IEnumerable<Person> people, IEnumerable<DriveLog> logs) 
        {
            var query = from log in logs
                        join person in people on log.PersonId equals person.Id
                        join car in cars on log.CarId equals car.Id
                        select new DriveLogQueryData
                        {
                            StartDateTime = log.StartDateTime,
                            EndDateTime = log.EndDateTime,
                            DriverName = person.Name,
                            CarManufacturer = car.Manufacturer,
                            CarModel = car.Model
                        };
            return query;
        }

        private IEnumerable<DriveLogQueryData> ProcessWithoutOptionals(IEnumerable<DriveLog> logs) 
        {
            var people = _personRepository.GetAll();
            var cars = _carRepository.GetAll();


            var logData = CreateLogData(cars, people, logs);
            return logData;
        }

        private IEnumerable<DriveLogQueryData> ProcessWithOptionals(IEnumerable<DriveLog> logs, OptionalParameters optionals) 
        {
            IEnumerable<Person> people;
            IEnumerable<Car> cars;
            var personName = optionals.DriverNameToFilterBy;
            var carName = optionals.CarModelToFilterBy;


            if (!String.IsNullOrEmpty(personName))
            {
                people = _personRepository.GetAll().Where(x => x.Name.Equals(personName));
            }
            else
            {
                people = _personRepository.GetAll();
            }

            if (!String.IsNullOrEmpty(carName))
            {
                cars = _carRepository.GetAll().Where(x => x.Model.Equals(carName));
            }
            else
            {
                cars = _carRepository.GetAll();
            }

            var logData = CreateLogData(cars, people, logs);

            if (optionals.MustSortByCar)
            {
                logData = logData.OrderBy(x => x.CarModel);
            }

            if (optionals.MustSortByDriver)
            {
                logData = logData.OrderBy(x => x.DriverName);
            }

            return logData.ToList();
        }
    }
}
